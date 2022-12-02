using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Schma.E3ProjectManager.Core.Application.Authorization;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.Mappings;
using Schma.E3ProjectManager.Core.Application.Queries;
using Schma.E3ProjectManager.Core.Application.Queries.Orders;

namespace Schma.E3ProjectManager.Core.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<PhoneNumberToStringConverter>();
            services.AddScoped<StringToPhoneNumberConverter>();

            services.AddAutoMapper(config =>
            {
                config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                config.AddProfile<UserProfile>();
                config.AddProfile<OrderProfile>();
            });
            services.AddMediator(x =>
            {
                #region Commands

                #region User

                x.AddConsumer<CreateUserCommandHandler>();
                x.AddConsumer<ActivateUserCommandHandler>();
                x.AddConsumer<DeactivateUserCommandHandler>();
                x.AddConsumer<AddRolesCommandHandler>();
                x.AddConsumer<RemoveRolesCommandHandler>();
                x.AddConsumer<ChangeUserPasswordCommandHandler>();
                x.AddConsumer<UpdateUserRolesCommandHandler>();
                x.AddConsumer<UpdateUserDetailsCommandHandler>();
                x.AddConsumer<CreateNewOrderCommandHandler>();

                #endregion User

                #endregion Commands

                #region Queries

                x.AddConsumer<GetAllUsersQueryHandler>();
                x.AddConsumer<GetUserQueryHandler>();
                x.AddConsumer<GetAllOrdersQueryHandler>();
                x.AddConsumer<GetOrderByIdQueryHandler>();

                #endregion
            });

            services.AddAuthorizationCore();
            services.AddAuthorizationPolicies();
        }

        private static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, UserOperationAuthorizationHandler>();
        }
    }
}