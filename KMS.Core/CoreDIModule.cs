using Autofac;
using KMS.Core.Interfaces;
using KMS.Core.Interfaces.Comment;
using KMS.Core.Interfaces.Role;
using KMS.Core.Interfaces.Trip;
using KMS.Core.Interfaces.User;
using KMS.Core.Services;
using KMS.Core.Services.Comment;
using KMS.Core.Services.Trip;
using Microsoft.AspNetCore.Http;

namespace KMS.Core;

public class CoreDIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CommentService>()
            .As<ICommentService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TripService>()
            .As<ITripService>()
            .InstancePerLifetimeScope();

        builder.Register<ICallerAccessor>(builder =>
        {
            var httpContext = builder.Resolve<IHttpContextAccessor>();
            var principal = httpContext.HttpContext?.User;
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return new CallerAccessor();
            }
            return new CallerAccessor(principal.Claims.ToList());
        }).InstancePerLifetimeScope();
    }
}
