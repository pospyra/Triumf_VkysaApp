using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triumf_VkysaApp.Forms
{
    public partial class RegistrationForm : Form
    {

        private DatabaseManager dbManager;

        public RegistrationForm()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lastName = familia.Text;
            string firstName = imya.Text;
            string patronymic = otchestvo.Text;
            string phone = telephone.Text;
            string hasCard = karta.Text;
            string login = loginBox.Text;
            string password = parol.Text;
            string accessRights = prava.Text;

            // Создание экземпляра ClientManager и вызов метода AddClient
            AddClient(lastName, firstName, patronymic, phone, hasCard, login, password, accessRights);

        }
        public void AddClient(string lastName, string firstName, string patronymic, string telephone, string hasCard, string login, string password, string accessRights)
        {
            string query = $"INSERT INTO Klient (Familia, Imya, Otshestvo, Telephone, Karta, Login, Parol, Prava_dostypa) " +
                           $"VALUES ('{lastName}', '{firstName}', '{patronymic}', '{telephone}', '{hasCard}', '{login}', '{password}', '{accessRights}')";

            try
            {
                dbManager.ExecuteCommand(query);
                MessageBox.Show("Клиент успешно добавлен!");
                // Дополнительные действия после добавления клиента
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Ошибка при добавлении клиента: " + ex.Message);
            }
        }
    }
}
