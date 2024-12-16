using Autofac;
using Autofac.Extensions.DependencyInjection;
using KMS.Core;
using KMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using KMS.Infrastructure.Data;
using KMS.API;
using KMS.API.Extensions;
using KMS.API.Middlewares;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using KMS.API.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using KMS.API.Configuration;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Utils.Library.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog(new NLogAspNetCoreOptions
{
    RemoveLoggerFactoryFilter = false,
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("KMSDatabase");
builder.Services.AddAppDbContext(connectionString);

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddHttpClient();

builder.Services.AddCors();

builder.Services
    .AddControllers(x =>
    {
        x.Filters.Add<TransactionPerRequestFilter>();
        x.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build()));
    })
    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.AddConverters())
    .AddFluentValidation(x =>
    {
        x.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly(), typeof(CoreDIModule).Assembly });
    }); ;

builder.Services.Configure<MvcOptions>(options =>
{
    TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
});

builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
builder.Services.AddAuthentication()
    .AddJwtBearer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KMS.API", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        },
        new string[] {}
    }
    });
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreDIModule());
    containerBuilder.RegisterModule(new InfrastructureDIModule());
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();


app.MapControllers();

MigrateDb(app);

app.Run();


static void MigrateDb(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();

            var seeder = new DatabaseSeeder(context);
            seeder.Seed();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred migrating the database.");
        }
    }
}

public partial class Program { }