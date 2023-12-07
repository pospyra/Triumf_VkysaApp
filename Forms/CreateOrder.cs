using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Triumf_VkysaApp.Forms
{
    public partial class CreateOrder : Form
    {
        private DatabaseManager databaseManager;
        private List<OrderItem> orders = new List<OrderItem>();

        public CreateOrder()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
            FillBlydo();
        }

        public void FillBlydo()
        {
            if (databaseManager != null)
            {
                databaseManager.Fill("Blydo", dataGridView1);
            }
        }

        public void PlaceOrder(int employeeCode, int tableCode, DateTime date, List<OrderItem> orderItems)
        {
            // Создание записи в таблице Zakaz
            string insertOrderQuery = "INSERT INTO Zakaz (Kod_sotrudnika, Kod_stolika, Dataa) VALUES (@EmployeeCode, @TableCode, @Date)";
            SqlParameter[] insertOrderParams =
            {
                new SqlParameter("@EmployeeCode", employeeCode),
                new SqlParameter("@TableCode", tableCode),
                new SqlParameter("@Date", date)
            };

            databaseManager.ExecuteCommand(insertOrderQuery, insertOrderParams);

            // Получение сгенерированного кода заказа
            string getLastInsertedIdQuery = "SELECT IDENT_CURRENT('Zakaz') AS LastInsertedId";
            int orderId = Convert.ToInt32(databaseManager.ExecuteScalar(getLastInsertedIdQuery));


            // Добавление блюд в заказ (таблица Zakaz_Blydo)
            string insertOrderItemsQuery = "INSERT INTO Zakaz_Blydo (Kod_zakaza, Kod_blyda, Kol_vo) VALUES (@OrderID, @DishCode, @Quantity)";
            foreach (OrderItem item in orderItems)
            {
                SqlParameter[] insertOrderItemsParams =
                {
                    new SqlParameter("@OrderID", orderId),
                    new SqlParameter("@DishCode", item.DishCode),
                    new SqlParameter("@Quantity", item.Quantity)
                };

                databaseManager.ExecuteCommand(insertOrderItemsQuery, insertOrderItemsParams);
            }

            MessageBox.Show("Заказ успешно оформлен!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            var newItem = new OrderItem
            {
                Quantity = int.Parse(textBox1.Text),
                DishCode = int.Parse(selectedID)
            };

            orders.Add(newItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int employeeCode = int.Parse(textBox3.Text);

            int tablecode = Data.SelectedStolik;

            var date = DateTime.UtcNow;

            var items = orders;

            PlaceOrder(employeeCode, tablecode, date, items);
        }
    }
}
