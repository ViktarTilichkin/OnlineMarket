using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineMarket.Extensions;
using OnlineMarket.Middleware;
using OnlineMarket.Options;

namespace OnlineMarket.Stratup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Получение ConnectionString из конфигурации
            var connectionString = Configuration.GetConnectionString("MySQLConnection");
            // Добавление сервисов, использующих ConnectionString
            services.AddRepositories(connectionString);
            services.AddServices();

            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidIssuer = AuthOptions.ISSUER, // строка, представляющая издателя
                            ValidateAudience = true, // будет ли валидироваться потребитель токена
                            ValidAudience = AuthOptions.AUDIENCE, // установка потребителя токена
                            ValidateLifetime = true, // будет ли валидироваться время существования
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                            ValidateIssuerSigningKey = true, // валидация ключа безопасности
                        };
                    });


            // Настройка сервисов, используемых в приложении (поговорим далее)
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                             }
                        });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Настройка middleware-компонентов, используемых в приложении (данные из файла Program)
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching(); // добавляем Middleware кэширования

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
