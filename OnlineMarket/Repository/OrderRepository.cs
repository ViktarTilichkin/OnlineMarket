using MySql.Data.MySqlClient;
using OnlineMarket.Models.Repository;

namespace OnlineMarket.Repository
{
    public class OrderRepository : BaseRepository
    {
        private const string SQL_GET_ALL = "select Id, User_Id, Product_Id, Date_Book, Count, Discount from order_product;";
        private const string SQL_GET_BY_ID = "select Id, User_Id, Product_Id, Date_Book, Count, Discount from order_product where id = {0};";
        public OrderRepository(MySqlConnection connect) : base(connect)
        {
        }
        public async Task<List<OrderProduct>> GetAll()
        {
            try
            {
                m_Connection.Open();
                MySqlCommand command = new MySqlCommand(SQL_GET_ALL, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                List<OrderProduct> files = new List<OrderProduct>();
                while (reader.Read())
                {
                    files.Add(new OrderProduct()
                    {
                        Id = reader.GetInt32(0),
                        User_Id = reader.GetInt32(1),
                        Product_Id = reader.GetInt32(2),
                        Date_Book = reader.GetDateTime(3),
                        Count = reader.GetInt32(4),
                        Discount = reader.GetInt32(5)
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
        public async Task<OrderProduct> GetById(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_GET_BY_ID, string.Join("", Id));
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                reader.Read();
                var result = new OrderProduct()
                {
                    Id = reader.GetInt32(0),
                    //Name = reader.GetString(1)
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
