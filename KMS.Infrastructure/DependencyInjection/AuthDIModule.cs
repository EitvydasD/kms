using Autofac;
using KMS.Core.Interfaces.Auth;
using KMS.Core.Services.Auth;

namespace KMS.Infrastructure.DependencyInjection;

public class AuthDIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AuthService>()
            .As<IAuthService>()
            .InstancePerLifetimeScope();
    }
}
