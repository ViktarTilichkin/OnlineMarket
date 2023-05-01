using MySql.Data.MySqlClient;
using OnlineMarket.Models.Repository;

namespace OnlineMarket.Repository
{
    public abstract class BaseRepository
    {
        protected readonly MySqlConnection m_Connection;
        public BaseRepository(MySqlConnection connect)
        {
            m_Connection = connect;
        }
    }
}
