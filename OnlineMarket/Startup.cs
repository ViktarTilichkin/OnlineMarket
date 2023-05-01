using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineMarket.Extensions;
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
            services.AddSwaggerGen();
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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
