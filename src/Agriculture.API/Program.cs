using Agriculture.Application;
using Agriculture.Infrastructure;
using Agriculture.Seeding;
using DotNetEnv;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Agriculture.API
{
    public partial class Program
    {
        private static async Task Main(string[] args)
        {
            Env.Load();

            var searchDir = Directory.GetCurrentDirectory();
            while (searchDir is not null)
            {
                var envPath = Path.Combine(searchDir, ".env");
                if (File.Exists(envPath))
                {
                    Env.Load(envPath); break;
                }
                searchDir = Directory.GetParent(searchDir)?.FullName;
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddSeeding();
            builder.Services.AddApplication();

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agriculture API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var otpSecret = builder.Configuration["OTP_SECRET_KEY"];


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.InitialiseDatabaseAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet("/api/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            {
                var endpoints = endpointSources.SelectMany(es => es.Endpoints);
                return endpoints.OfType<RouteEndpoint>().Select(e => new
                {
                    Method = e.Metadata.OfType<HttpMethodMetadata>()
                                        .FirstOrDefault()?
                                        .HttpMethods.FirstOrDefault(),
                    Route = e.RoutePattern.RawText
                });
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}