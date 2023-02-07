using Autofac;
using KMS.Infrastructure.Data;
using KMS.Infrastructure.DependencyInjection;
using Utils.Library.Interfaces;
using Module = Autofac.Module;

namespace KMS.Infrastructure;

public class InfrastructureDIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
           .As(typeof(IRepository<>))
           .As(typeof(IReadRepository<>))
           .InstancePerLifetimeScope();

        builder.RegisterModule<AuthDIModule>();
        builder.RegisterModule<UserDIModule>();
    }
}
