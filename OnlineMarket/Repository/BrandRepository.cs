using MySql.Data.MySqlClient;
using OnlineMarket.Models.Repository;
using System.Data.Common;

namespace OnlineMarket.Repository
{
    public class BrandRepository : BaseRepository
    {
        private const string SQL_GET_ALL = "select Id, Name from brand;";
        private const string SQL_GET_BY_ID = "select Id, Name from brand where id = {0};";
        public BrandRepository(MySqlConnection connect) : base(connect)
        {
        }
        public async Task<List<Brand>> GetAll()
        {
            try
            {
                m_Connection.Open();
                MySqlCommand command = new MySqlCommand(SQL_GET_ALL, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                List<Brand> files = new List<Brand>();
                while (reader.Read())
                {
                    files.Add(new Brand()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                return files;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
        }
        public async Task<Brand> GetById(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_GET_BY_ID, string.Join("", Id));
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                reader.Read();
                var result = new Brand()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_Connection.Close();
            }
        }

    }
}
