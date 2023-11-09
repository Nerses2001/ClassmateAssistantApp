using AutoMapper;
using ClassmateAssistantApp.Filters;
using ClassmateAssistantApp.Middleware;
using DataLayer;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Model.Mapper;

namespace ClassmateAssistantApp.Extansions
{
    public static class ServiceCollectionExtansion
    {
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(

             mc =>
             {
                 mc.AddProfile(new ApplicationUserProfile());
             }
         );

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            return services;
        }


        public static IServiceCollection AddIdentityServers(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>
                (c =>
                {
                    c.Password.RequireDigit = true;
                    c.Password.RequireLowercase = true;
                    c.Password.RequiredLength = 8;
                    c.Password.RequireNonAlphanumeric = true;
                    c.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            return services;

        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HouseBookingAPI", Description = "Description for House Booking API", Version = "v1" });
                c.OperationFilter<AddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });


                c.DocumentFilter<SwaggerAddEnumDescriptionsFilter>();
            });

            return services;
        }
    }
}
