using Domain.Customers.Repositories;
using Domain.Localization;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using Domain.Reservations.Repositories;
using Domain.Shared.Repositories;
using Infrastructure.Authentication.JWTProvider;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.Authorization.AuthorizationHandlers;
using Infrastructure.Authorization.AuthorizationPolicyProvider;
using Infrastructure.BackgroundJobs;
using Infrastructure.CustomersPersistance.Repository;
using Infrastructure.DataAccess.MealsInformationPersistance;
using Infrastructure.DataAccess.PermissionsService;
using Infrastructure.DataAccess.UnitOfWork;
using Infrastructure.DataAccess.UserPersistence;
using Infrastructure.DomainLocalization;
using Infrastructure.ForgetPasswordHandling.ForgetPasswordServices;
using Infrastructure.ForgetPasswordHandling.Repository;
using Infrastructure.ForgetPasswordHandling.VerificationCodeGenerators;
using Infrastructure.Mail.Abstraction;
using Infrastructure.Mail.HiastMail;
using Infrastructure.MealsPersistence.Repository;
using Infrastructure.Notification;
using Infrastructure.Notification.Services;
using Infrastructure.PricingRecordsPersistance.Repository;
using Infrastructure.ReservationsPersistence.Repository;
using Localization.LocalizationBuilders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SharedKernal.Repositories;

namespace Infrastructure.DependencyInjectionConfiguration;
public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IMealInformationRepository, MealInformationRepository>();
		services.AddScoped<IMealEntryRepository, MealEntryRepository>();
		services.AddScoped<IReservationRepository, ReservationRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IPricingRepository, PricingRepository>();
		services.AddScoped<IUserPersistenceService, UserPersistenceService>();
		services.AddScoped<IHashHandler, HashHandler>();
		services.AddScoped<IPermissionService, PermissionService>();
		services.AddScoped<IJwtProvider, JwtProvider>();
		services.AddSingleton<IAuthorizationHandler,
			PermissionAuthorizationHandler>();
		services.AddSingleton<IAuthorizationPolicyProvider,
			PermissionAuthorizationPolicyProvider>();
		services.AddSignalR();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<IEmailSender, HiastMailSender>();
		services.AddScoped<IForgetPasswordService, ForgetPasswordService>();
		services.AddScoped<IVerificationCodeGenerator, VerificationCodeGenerator>();
		services.AddScoped<IForgetPasswordRepository, ForgetPasswordRepository>();
		services.AddScoped<IDomainLocalizer, DomainLocalizer>();
		services.AddScoped(typeof(LocalizationBuilder));
		services.AddQuartz(options =>
		{
			options.UseMicrosoftDependencyInjectionJobFactory();
			
		});
		services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

		services.ConfigureOptions<ReservationCollectorBackgroundJobSetup>();
	}
}
