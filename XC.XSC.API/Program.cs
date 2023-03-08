using Microsoft.OpenApi.Models;
using NLog;
using XC.XSC.API.Extentions;
using XC.XSC.Extentions.GlobalErrorHandling;
using XC.XSC.Utilities.Keycloak;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using XC.CCMP.KeyVault;
using XC.XSC.Data;
using System.Reflection;
using XC.XSC.ValidateMail.Models.Request;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddHttpClient();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "XC - Smart Clear API", Version = "1.0.0.1" });
        var xmlFiles = new[]
         {
            //the current assembly (XC.XSC.API)
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",

            //another assembly (XC.XSC.ValidateMail)
            $"{Assembly.GetAssembly(typeof(ValidateMailScopeRequest))?.GetName().Name}.xml"
        };

        foreach (var xmlFile in xmlFiles)
        {
            var xmlCommentFile = xmlFile;
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            if (File.Exists(xmlCommentsFullPath))
            {
                c.IncludeXmlComments(xmlCommentsFullPath);
            }
        }

        c.AddSecurityDefinition("Bearer", //Name the security scheme
            new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
            });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddMvc();

    builder.Services.AddCors(o => o.AddPolicy("xsc_Policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    //builder.Services.AddAutoMapper(typeof(Program).Assembly);

    builder.Services.AddAzureClients(azureClientFactoryBuilder =>
    {
        azureClientFactoryBuilder.AddSecretClient(
        builder.Configuration.GetSection("KeyVault"));
    });

    LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

    //Add dependency injection
    builder.Services.AddDependencies();
    builder.Services.AddAutoMapperProfiles();

    
    //Add keycloak authentication
    builder.Services.AddKeycloakAuthentication();

}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("xsc_Policy");
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    // global error handler
    app.UseMiddleware<ExceptionMiddleware>();

    app.MapControllers();

    app.Run();
}