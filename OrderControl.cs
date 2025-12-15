using demoNEW_EFCoreVersion.DatabaseModel;
using Microsoft.EntityFrameworkCore;
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
    internal partial class OrderControl : UserControl
    {
        private Order _order;

        public OrderControl(Order order)
        {
            InitializeComponent();
            _order = order;
            DisplayOrder();

            // Проверяем роль для показа кнопок редактирования/удаления
            if (UserSession.Role != "Администратор")
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void DisplayOrder()
        {
            lblArticle.Text = $"Номер заказа: {_order.Id}";
            lblStatus.Text = $"Статус заказа: {_order.OrderStatus?.Name}";
            lblPickupAddress.Text = $"Адрес пункта выдачи: {_order.PickupPoint?.City}, {_order.PickupPoint?.Street}, {_order.PickupPoint?.House} (Индекс: {_order.PickupPoint?.Index})";
            lblDeliveryDate.Text = $"Дата заказа: {_order.DateOrder.ToShortDateString()}";

            // Для поля "Дата доставки" справа - используем lblDeliveryDateRight
            lblDeliveryDateRight.Text = _order.DateDelivery.ToShortDateString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var editForm = new OrderEditForm(_order))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Перезагружаем данные заказа
                    using (var context = new ApplicationContext())
                    {
                        _order = context.Orders
                            .Include(o => o.OrderStatus)
                            .Include(o => o.PickupPoint)
                            .Include(o => o.Users)
                            .FirstOrDefault(o => o.Id == _order.Id);
                        DisplayOrder();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var context = new ApplicationContext())
                {
                    var orderToDelete = context.Orders.FirstOrDefault(o => o.Id == _order.Id);
                    if (orderToDelete != null)
                    {
                        context.Orders.Remove(orderToDelete);
                        context.SaveChanges();
                        this.Parent.Controls.Remove(this); // Удаляем контроль из FlowLayoutPanel
                    }
                }
            }
        }
    }
}
