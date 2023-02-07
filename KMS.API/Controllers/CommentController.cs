using KMS.API.Models.Comment;
using KMS.API.Models.User;
using KMS.Core;
using KMS.Core.Aggregates.Comment.Entities;
using KMS.Core.Aggregates.Comment.Requests;
using KMS.Core.Aggregates.Role;
using KMS.Core.Interfaces.Comment;
using Microsoft.AspNetCore.Mvc;

namespace KMS.API.Controllers;

[ApiController]
[PermissionsRequired(nameof(PermissionId.CommentView))]
[Route("api/comment")]
public class CommentController : BaseController
{
    public CommentController(ICommentService commentService)
    {
        CommentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }

    private ICommentService CommentService { get; }

    [HttpGet]
    [HttpGet("parentId/{parentId:Guid}")]
    public async Task<ICollection<CommentModel>> GetComments(Guid? parentId = null, CancellationToken cancellationToken = default)
    {
        var response = await CommentService.GetComments(parentId, cancellationToken);
        return Mapper.Map<ICollection<CommentModel>>(response);
    }

    [PermissionsRequired(nameof(PermissionId.CommentPost))]
    [HttpPost]
    public async Task<CommentModel> PostComment([FromBody] PostCommentRequest request, CancellationToken cancellationToken = default)
    {
        var response = await CommentService.PostComment(request, cancellationToken);
        return Mapper.Map<CommentModel>(response);
    }
}
