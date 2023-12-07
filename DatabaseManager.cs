using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Triumf_VkysaApp
{
    public class DatabaseManager
    {
        private SqlConnection connection;
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\недопрограммирование\Triumf_VkysaApp\DataBase\Triumf_Vkysa.mdf"";Integrated Security=True;Connect Timeout=30";

        public DatabaseManager()
        {
            connection = new SqlConnection(connectionString);
        }

        public void Fill(string tableName, DataGridView grid)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand($"SELECT  * FROM [{tableName}]", conn);
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            grid.DataSource = table;
        }

        // Метод для получения данных из базы данных
        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        // Метод для выполнения команд SQL (INSERT, UPDATE, DELETE)


        public void ExecuteCommand(string command, SqlParameter[] parameters = null)
        {
            SqlCommand cmd = new SqlCommand(command, connection);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public object ExecuteScalar(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }


        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procedureName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
