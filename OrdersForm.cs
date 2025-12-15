﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace demoNEW_EFCoreVersion
{
    public partial class OrdersForm : Form
    {
        private class ComboItem
        {
            public int Id { get; }
            public string Name { get; }
            public ComboItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }

        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            // Настройка фильтров
            InitFilters();
            ApplyFilters();
        }

        private void InitFilters()
        {
            using (var context = new ApplicationContext())
            {
                // Статусы
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Все статусы");
                foreach (var s in context.OrderStatuses.AsNoTracking().OrderBy(x => x.Name).ToList())
                    cmbStatus.Items.Add(new ComboItem(s.Id, s.Name));
                cmbStatus.SelectedIndex = 0;

                // Пункты выдачи
                cmbPickupPoint.Items.Clear();
                cmbPickupPoint.Items.Add("Все пункты");
	                // У пунктов выдачи нет поля Name, поэтому делаем понятную строку сами
	                foreach (var p in context.PickupPoints.AsNoTracking()
	                             .OrderBy(x => x.City)
	                             .ThenBy(x => x.Street)
	                             .ThenBy(x => x.House)
	                             .ToList())
	                {
	                    var text = $"{p.Index}, {p.City}, {p.Street}, {p.House}";
	                    cmbPickupPoint.Items.Add(new ComboItem(p.Id, text));
	                }
                cmbPickupPoint.SelectedIndex = 0;
            }
            // Фильтр по датам (можно отключать)
            dtpDateFrom.ShowCheckBox = true;
            dtpDateTo.ShowCheckBox = true;
            dtpDateFrom.Checked = false;
            dtpDateTo.Checked = false;

            // Авто-применение фильтров
            txtSearch.TextChanged += (_, __) => ApplyFilters();
            cmbStatus.SelectedIndexChanged += (_, __) => ApplyFilters();
            cmbPickupPoint.SelectedIndexChanged += (_, __) => ApplyFilters();
            dtpDateFrom.ValueChanged += (_, __) => ApplyFilters();
            dtpDateTo.ValueChanged += (_, __) => ApplyFilters();
            chkActualOnly.CheckedChanged += (_, __) => ApplyFilters();

            btnResetFilters.Click += (_, __) =>
            {
                txtSearch.Text = "";
                cmbStatus.SelectedIndex = 0;
                cmbPickupPoint.SelectedIndex = 0;
                dtpDateFrom.Checked = false;
                dtpDateTo.Checked = false;
                chkActualOnly.Checked = false;
                ApplyFilters();
            };
        }

        private void ApplyFilters()
        {
            flowLayoutPanelOrders.Controls.Clear();

            var search = (txtSearch.Text ?? "").Trim();
            var selectedStatus = cmbStatus.SelectedItem as ComboItem;
            var selectedPickup = cmbPickupPoint.SelectedItem as ComboItem;

            using (var context = new ApplicationContext())
            {
                // Базовый запрос
                var query = context.Orders
                    .Include(o => o.OrderStatus)
                    .Include(o => o.PickupPoint)
                    .Include(o => o.Users)
                    .AsNoTracking()
                    .AsQueryable();

                // Поиск: ID / логин / ФИО / код получения
                // Поиск (id / код / пользователь)
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (int.TryParse(search, out var id))
                        query = query.Where(o => o.Id == id);

                    query = query.Where(o =>
                        o.ReceiptCode.Contains(search) ||
                        o.Users.Login.Contains(search) ||
                        o.Users.LastName.Contains(search) ||
                        o.Users.FirstName.Contains(search) ||
                        o.Users.Patronymic.Contains(search));
                }

                // Статус

                if (selectedStatus != null)
                    query = query.Where(o => o.OrderStatusId == selectedStatus.Id);

                // Пункт выдачи

                if (selectedPickup != null)
                    query = query.Where(o => o.PickupPointId == selectedPickup.Id);

                // Диапазон дат (по дате оформления)
                // Диапазон дат оформления
                if (dtpDateFrom.Checked)
                {
                    var from = dtpDateFrom.Value.Date;
                    query = query.Where(o => o.DateOrder >= from);
                }

                if (dtpDateTo.Checked)
                {
                    var toExclusive = dtpDateTo.Value.Date.AddDays(1);
                    query = query.Where(o => o.DateOrder < toExclusive);
                }

                // Только актуальные
                if (chkActualOnly.Checked)
                {
                    var today = DateTime.Today;
                    query = query.Where(o => o.DateDelivery >= today);
                }

                var orders = query
                    .OrderByDescending(o => o.DateOrder)
                    .ToList();

                // Перерисовываем список
                foreach (var order in orders)
                {
                    var orderControl = new OrderControl(order);
                    flowLayoutPanelOrders.Controls.Add(orderControl);
                }
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using (var editForm = new OrderEditForm(null))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilters();
                }
            }
        }
    }
}
