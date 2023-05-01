using MySql.Data.MySqlClient;
using OnlineMarket.Models.Repository;
using System.Data.Common;

namespace OnlineMarket.Repository
{
    public class CategoryRepository : BaseRepository
    {
        private const string SQL_GET_ALL = "select Id, Name from category;";
        private const string SQL_GET_BY_ID = "select Id, Name from category where id = {0};";
        public CategoryRepository(MySqlConnection connect) : base(connect)
        {
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                m_Connection.Open();
                MySqlCommand command = new MySqlCommand(SQL_GET_ALL, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                List<Category> files = new List<Category>();
                while (reader.Read())
                {
                    files.Add(new Category()
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
        public async Task<Category> GetById(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_GET_BY_ID, string.Join("", Id));
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                reader.Read();
                var result = new Category()
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
