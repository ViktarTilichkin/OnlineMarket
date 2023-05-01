using MySql.Data.MySqlClient;
using OnlineMarket.Models.Repository;

namespace OnlineMarket.Repository
{
    public class ProductRepository : BaseRepository
    {
        private const string SQL_GET_ALL = "select Id, Name, Description, Price, Category_id, Brand_Id from product;";
        private const string SQL_GET_BY_ID = "select Id, Name, Description, Price, Category_id, Brand_Id from product where id = {0};";
        public ProductRepository(MySqlConnection connect) : base(connect)
        {
        }
        public async Task<List<Product>> GetAll()
        {
            try
            {
                m_Connection.Open();
                MySqlCommand command = new MySqlCommand(SQL_GET_ALL, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                List<Product> files = new List<Product>();
                while (reader.Read())
                {
                    files.Add(new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        Category_id = reader.GetInt32(4),
                        Brand_Id = reader.GetInt32(5)
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
        public async Task<Product> GetById(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_GET_BY_ID, string.Join("", Id));
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                reader.Read();
                var result = new Product()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Category_id = reader.GetInt32(4),
                    Brand_Id = reader.GetInt32(5)
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
