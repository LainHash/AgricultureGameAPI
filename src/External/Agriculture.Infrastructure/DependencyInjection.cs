using Agriculture.Application.Services.Business;
using Agriculture.Domain.Repositories;
using Agriculture.Infrastructure.Context;
using Agriculture.Infrastructure.Repositories;
using Agriculture.Infrastructure.Repositories.Territory;
using Agriculture.Infrastructure.Sevices.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agriculture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── Database ─────────────────────────────────────────────────────
            services.AddDbContext<AgricultureDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("MyConnectString"),
                    sqlOptions => sqlOptions.MigrationsAssembly(
                        typeof(AgricultureDbContext).Assembly.FullName)));

            // ── AutoMapper ───────────────────────────────────────────────────
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            // ── Cloudinary ───────────────────────────────────────────────────
            //services.Configure<CloudinarySettings>(
            //    configuration.GetSection(CloudinarySettings.SectionName));
            //services.AddScoped<ICloudinaryService, CloudinaryService>();

            // ── Repositories ─────────────────────────────────────────────────
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var assembly = typeof(FarmRepository).Assembly;

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                if (!type.Name.EndsWith("Repository"))
                    continue;

                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.Name.EndsWith("Repository"))
                    {
                        services.AddScoped(iface, type);
                    }
                }
            }

            // ── Services ─────────────────────────────────────────────────────
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDataImporter, ExcelImporter>();

            // ── Authentication & Security ────────────────────────────────────
            //services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            //services.AddScoped<IJwtProvider, JwtProvider>();
            //services.AddScoped<IPasswordHasher, PasswordHasher>();
            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IOtpHasher, OtpHasher>();

            //var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            //if (jwtSettings is not null)
            //{
            //    services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = jwtSettings.Issuer,
            //            ValidAudience = jwtSettings.Audience,
            //            IssuerSigningKey = new SymmetricSecurityKey(
            //                System.Text.Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            //        };
            //    });
            //}

            return services;
        }
    }
}
