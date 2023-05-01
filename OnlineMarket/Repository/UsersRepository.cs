using System.Text;
using MySql.Data.MySqlClient;
using OnlineMarket.Models;

namespace OnlineMarket.Repository
{
    public class UsersRepository
    {

        private readonly MySqlConnection m_Connection;
        private const string SQL_GET_ALL = "select Id, Name, Surname, Email, Pwd from users;";
        private const string SQL_GET_BY_ID = "select Id, Name, Surname, Email, Pwd from users where id = {0};";
        private const string SQL_CREATE = "insert into users(Name, Surname, Email, Pwd) values(?, ?, ?, ?);";
        private const string SQL_UPDATE = "update users set Name = ?, Surname = ?, Email = ?, Pwd = ? where email = ?;";
        private const string SQL_DELETE = "delete from users where id = {0};";
        public UsersRepository(MySqlConnection connect)
        {
            m_Connection = connect;
        }
        public async Task<List<User>> GetAll()
        {
            try
            {
                m_Connection.Open();
                MySqlCommand command = new MySqlCommand(SQL_GET_ALL, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                List<User> files = new List<User>();
                while (reader.Read())
                {
                    files.Add(new User()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Email = reader.GetString(3),
                        Pwd = reader.GetString(4)
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
        public async Task<User> GetById(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_GET_BY_ID, string.Join("", Id));
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                var reader = await command.ExecuteReaderAsync();
                reader.Read();
                var result = new User()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Email = reader.GetString(3),
                    Pwd = reader.GetString(4)
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

        public async Task<User> Create(User user)
        {
            try
            {
                m_Connection.Open();
                DynamicQuery(m_Connection, SQL_CREATE, user.Name, user.Surname, user.Email, user.Pwd);
                return user;
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
        public async Task<User> Update(User user)
        {
            try
            {
                m_Connection.Open();
                //var sql = string.Format(SQL_UPDATE, user.Email);
                //MySqlCommand command = new MySqlCommand(sql, m_Connection);
                //command.Parameters.AddWithValue($"@name", user.Name);
                //command.Parameters.AddWithValue($"@surname", user.Surname);
                //command.Parameters.AddWithValue($"@email", user.Email);
                //command.Parameters.AddWithValue($"@pwd", user.Pwd);
                //command.ExecuteNonQuery();
                DynamicQuery(m_Connection, SQL_UPDATE, user.Name, user.Surname, user.Email, user.Pwd, user.Email);
                return user;
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
        public async Task<bool> Delete(int Id)
        {
            try
            {
                m_Connection.Open();
                var sql = string.Format(SQL_DELETE, Id);
                MySqlCommand command = new MySqlCommand(sql, m_Connection);
                command.ExecuteNonQuery();
                return true;
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
        private void DynamicQuery(MySqlConnection conn, string script, params object[] item)
        {
            int startIndex = 0;
            int i = 0;
            var sb = new StringBuilder();
            while (startIndex < script.Length)
            {
                var endIndex = script.IndexOf('?', startIndex);
                if (endIndex == -1)
                {
                    sb.Append(script[startIndex..]);
                    break;
                }
                sb.Append(script[startIndex..endIndex]); //sb.Append(script.Substring(startIndex, endIndex - startIndex));
                sb.Append($"@pr{i++}");
                startIndex = endIndex + 1;
            }
            MySqlCommand command = new MySqlCommand(sb.ToString(), conn);
            for (i = 0; i < item.Length; i++)
            {
                command.Parameters.AddWithValue($"@pr{i}", item[i]);
            }
            command.ExecuteNonQuery();
        }
    }
}
