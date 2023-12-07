using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Triumf_VkysaApp.Forms.Admin
{
    public partial class CreateBlydo : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\недопрограммирование\Triumf_VkysaApp\DataBase\Triumf_Vkysa.mdf"";Integrated Security=True;Connect Timeout=30";

        public CreateBlydo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dishName = textBox1.Text;
            decimal cost = decimal.Parse(textBox2.Text);
            decimal weight = decimal.Parse(textBox3.Text);
            string category = textBox5.Text;


            AddBlydo(dishName, cost, weight, category);

        }
        public void AddBlydo(string dishName, decimal cost, decimal weight, string category)
        {
            string query = "INSERT INTO Blydo (Naz_blyda, Stoimost, Ves, Bludo_mes, Kategory) " +
                   "VALUES (@dishName, @cost, @weight, @bludoMes, @category)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@dishName", dishName);
                    command.Parameters.AddWithValue("@cost", cost);
                    command.Parameters.AddWithValue("@weight", weight);
                    command.Parameters.AddWithValue("@bludoMes", DBNull.Value); // Assuming Bludo_mes allows NULLs
                    command.Parameters.AddWithValue("@category", category);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Блюдо успешно добавлено!");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить блюдо.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении блюда: " + ex.Message);
            }
        }
    }
}
