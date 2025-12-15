using demoNEW_EFCoreVersion.DatabaseModel;
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
    internal partial class OrderEditForm : Form
    {
        private Order _order;
        private bool _isNew;
        private readonly int _originalId;

        public OrderEditForm(Order order)
        {
            InitializeComponent();
            _order = order ?? new Order();
            _isNew = order == null;
            _originalId = order?.Id ?? 0;
            LoadCombos();
            DisplayOrder();
        }

        private void LoadCombos()
        {
            using (var context = new ApplicationContext())
            {
                cmbPickupPoint.DataSource = context.PickupPoints.ToList();
                cmbPickupPoint.DisplayMember = "City"; // Или комбинацию адреса
                cmbPickupPoint.ValueMember = "Id";

                cmbUser.DataSource = context.Users.ToList();
                cmbUser.DisplayMember = "Login"; // Или FullName
                cmbUser.ValueMember = "Id";

                cmbOrderStatus.DataSource = context.OrderStatuses.ToList();
                cmbOrderStatus.DisplayMember = "Name";
                cmbOrderStatus.ValueMember = "Id";
            }
        }

        private void DisplayOrder()
        {
            // Для нового заказа задаём адекватные значения, чтобы DateTimePicker не упал на MinValue
            dtpDateOrder.Value = _order.DateOrder == default(DateTime) ? DateTime.Today : _order.DateOrder;
            dtpDateDelivery.Value = _order.DateDelivery == default(DateTime) ? DateTime.Today.AddDays(1) : _order.DateDelivery;
            cmbPickupPoint.SelectedValue = _order.PickupPointId;
            cmbUser.SelectedValue = _order.UsersId;
            txtReceiptCode.Text = _order.ReceiptCode;
            cmbOrderStatus.SelectedValue = _order.OrderStatusId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация (простая)
            if (dtpDateOrder.Value >= dtpDateDelivery.Value)
            {
                MessageBox.Show("Дата заказа должна быть раньше даты доставки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new ApplicationContext())
            {
                if (_isNew)
                {
                    _order.DateOrder = dtpDateOrder.Value;
                    _order.DateDelivery = dtpDateDelivery.Value;
                    _order.PickupPointId = (int)cmbPickupPoint.SelectedValue;
                    _order.UsersId = (int)cmbUser.SelectedValue;
                    _order.ReceiptCode = txtReceiptCode.Text;
                    _order.OrderStatusId = (int)cmbOrderStatus.SelectedValue;
                    context.Orders.Add(_order);
                }
                else
                {
                    // Обновляем сущность из текущего контекста (без Update(detached)).
                    var dbOrder = context.Orders.FirstOrDefault(o => o.Id == _originalId);
                    if (dbOrder == null)
                    {
                        MessageBox.Show("Не удалось найти заказ для обновления (возможно, он был удалён).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dbOrder.DateOrder = dtpDateOrder.Value;
                    dbOrder.DateDelivery = dtpDateDelivery.Value;
                    dbOrder.PickupPointId = (int)cmbPickupPoint.SelectedValue;
                    dbOrder.UsersId = (int)cmbUser.SelectedValue;
                    dbOrder.ReceiptCode = txtReceiptCode.Text;
                    dbOrder.OrderStatusId = (int)cmbOrderStatus.SelectedValue;

                    _order = dbOrder;
                }
                context.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
