using Application.AssemblyReference;
using Application.Behaviors;
using Application.ExecutorProvider;
using Domain.AssemblyReference;
using Infrastructure.AssemblyReference;
using Infrastructure.DataAccess.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.IdentityChecker;
using Presentation.Mappers;
using Presentation.OptionsSetup.BackGroundJobsSetup;
using Presentation.OptionsSetup.JWTOptionsSetup;
using Presentation.OptionsSetup.MailAccountSetup;
using Presentation.OptionsSetup.PipLineOptionsSetup;
using Presentation.OptionsSetup.SettingsSetup;
using Presentation.OptionsSetup.SmtpServerSetup;
using Presentation.OptionsSetup.VerificationCodeSetup;
using Presentation.Services.MealsImagesSaver;
using System.Reflection;
using System.Text;

namespace Presentation.DependencyInjectionConfiguration;
public static class DependencyInjection
{
	public static void ConfigureSystemOptions(this IServiceCollection services)
	{
		// options
		services.ConfigureOptions<JwtOptionsSetup>();
		services.ConfigureOptions<ApiSettingsSetup>();
		services.ConfigureOptions<PipelineOptionsSetup>();
		services.ConfigureOptions<SmtpServerSetUp>();
		services.ConfigureOptions<MailAccountSetUp>();
		services.ConfigureOptions<VerificationCodeSetUp>();
		services.ConfigureOptions<ScheduleForCollectPassedReservationsSettingsSetUp>();
	}
	public static void AddPresentation(this IServiceCollection services)
	{
		
		//// Presentation level
		services.AddScoped<IMapper, Mapper>();


		// ISingletonReservationQueue

		services.AddScoped<IAssetsSaver, AssetsSaver>();
		services.AddSingleton<DomainEventsCollectorInterceptor>();








		services.AddScoped<IExecutorIdentityProvider, JWTUserIdentityProvider>();
		services.AddHttpContextAccessor();






		Assembly[] assembliesForConfigureMediatR = new Assembly[]
		{
			typeof(ApplicationAssemblyReference).Assembly,
			typeof(InfrastructureAssemblyReference).Assembly,
			typeof(DomainAssemblyReference).Assembly
		};

		services.AddMediatR(assembliesForConfigureMediatR)
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeLineBehaviorForLogableCommands<,>))
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeLineBehaviorForLogableQueries<,>));




		//services.AddScoped(
		//	typeof(IPipelineBehavior<ILogableCommand<Result>, Result>),
		//	typeof(LogableCommandLoggingPipeLineBehavior <ILogableCommand<Result>, Result>));

		//services.AddScoped(
		//	typeof(IPipelineBehavior<ILogableQuery<Result<object>>, Result<object>>),
		//	typeof(LogableQueryLoggingPipeLineBehavior<ILogableQuery<Result<object>>, Result<object>, object>));


		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Auth",
				Version = "v1"
			});
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "please",
				Name = "auth",
				Type = SecuritySchemeType.ApiKey,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});
			c.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
		{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference()
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{ }
		}
			});
		});

		// Hashing 


		// JWT

		//builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

		//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		//	.AddJwtBearer(options=>
		//				  options.TokenValidationParameters = new()
		//				  {
		//				  	ValidateIssuer = false,
		//				  	//ValidateAudience = true,
		//				  	ValidateLifetime = true,
		//				  	ValidateIssuerSigningKey = true,
		//				  	ValidIssuer = "Alkhall",
		//				  	ValidAudience = "Jaffar",
		//				  	IssuerSigningKey =
		//				  		new SymmetricSecurityKey(Encoding.UTF8.
		//				  		GetBytes("_AboRamezSecretKey12345"))
		//				  }
		//);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})

					   .AddJwtBearer(options =>
					   {
						   options.TokenValidationParameters = new TokenValidationParameters
						   {
							   ValidateIssuerSigningKey = true,
							   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AboRamezSecretKey12345")),
							   ValidateIssuer = false,
							   ValidateAudience = false,
							   RequireExpirationTime = false,
							   ValidateLifetime = true
						   };
					   });

		services.AddAuthorization();
	}
}
