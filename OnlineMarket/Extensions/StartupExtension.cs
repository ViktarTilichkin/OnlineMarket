using MySql.Data.MySqlClient;
using OnlineMarket.Repository;
using OnlineMarket.Services;

namespace OnlineMarket.Extensions
{
    public static class StartupExtension
    {
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(_ => new MySqlConnection(connectionString));
            services.AddTransient<UsersRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {

            services.AddTransient<UserService>();
            services.AddTransient<AccountService>();
        }
    }
}

