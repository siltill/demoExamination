using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demoNEW_EFCoreVersion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Отображаем ФИО пользователя из UserSession
            lblFullName.Text = $"Пользователь: {UserSession.FullName} ({UserSession.Role})";
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // Открываем форму с продуктами 
            using (var productsForm = new ProductsForm()) // Замените на вашу форму продуктов
            {
                productsForm.ShowDialog();
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            // Открываем форму с заказами 
            using (var ordersForm = new OrdersForm())
            {
                ordersForm.ShowDialog();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Очищаем сессию и возвращаемся к форме авторизации
            UserSession.Clear();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
