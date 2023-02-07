using AutoMapper;
using KMS.API.Models.Comment;
using KMS.API.Models.Role;
using KMS.API.Models.Trip;
using KMS.API.Models.User;
using KMS.Core.Aggregates.Comment.Entities;
using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Aggregates.User.Models;

namespace KMS.API;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(x => x.Roles.Select(x => x.Role)));
        CreateMap<UserEntity, BaseUserModel>();
        CreateMap<RoleEntity, RoleModel>();
        CreateMap<CommentEntity, CommentModel>();
        CreateMap<TripEntity, TripModel>()
            .ForMember(dest => dest.Responsible, opt => opt.MapFrom(x => x.Responsible.Select(x => x.User)));
    }
}
