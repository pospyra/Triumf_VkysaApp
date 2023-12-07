using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Triumf_VkysaApp.Forms
{
    public partial class UserPanel : Form
    {
        private DatabaseManager databaseManager;

        public UserPanel()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
            FillStoliks();
        }

        public void FillStoliks()
        {
            if (databaseManager != null)
            {
                databaseManager.Fill("Stolik", dataGridView1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tableId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            var klientId = Data.KlientId;

            UpdateTableWithCustomer(tableId, klientId);
        }

        public void UpdateTableWithCustomer(int tableId, int customerId)
        {
            string updateTableQuery = "UPDATE Stolik SET Kod_klienta = @CustomerId WHERE Kod_stolika = @TableId";

            SqlParameter[] updateTableParams =
            {
                new SqlParameter("@CustomerId", customerId),
                new SqlParameter("@TableId", tableId)
            };

            databaseManager.ExecuteCommand(updateTableQuery, updateTableParams);

            Data.SelectedStolik = tableId;

            new Forms.CreateOrder().Show();
            Hide();
        }
    }
}
