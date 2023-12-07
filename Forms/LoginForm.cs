using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Triumf_VkysaApp.Forms
{
    public partial class LoginForm : Form
    {
        private DatabaseManager databaseManager;

        public LoginForm()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var pass = textBox2.Text;
            var isAuth = AuthenticateUser(login, pass);
            if (isAuth)
            {
                new Forms.UserPanel().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации");
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            string query = "SELECT * FROM Klient WHERE Login = @Username AND Parol = @Password";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            DataTable result = databaseManager.GetData(query, parameters);

            // Проверяем результат выполнения запроса
            if (result != null && result.Rows.Count > 0)
            {
                int userId = Convert.ToInt32(result.Rows[0][0]);
                Data.KlientId = userId;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Forms.RegistrationForm().Show();
            Hide();
        }
    }
}
