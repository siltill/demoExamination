﻿using Microsoft.EntityFrameworkCore;
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
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            // Настройка фильтров
            InitFilters();
            ApplyFilters();
            // Кнопка добавления только для админа
            if (UserSession.Role != "Администратор")
            {
                btnAddProduct.Visible = false;
            }
        }

        
        private sealed class ComboItem
        {
            public int Id { get; }
            public string Name { get; }
            public ComboItem(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public override string ToString() => Name;
        }

        private void InitFilters()
        {
            // Заполняем выпадающие списки и вешаем обработчики.
            using (var context = new ApplicationContext())
            {
                // Категории
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Все категории");
                foreach (var c in context.ProductCategories.AsNoTracking().OrderBy(x => x.Name).ToList())
                    cmbCategory.Items.Add(new ComboItem(c.Id, c.Name));
                cmbCategory.SelectedIndex = 0;

                // Производители
                cmbManufacturer.Items.Clear();
                cmbManufacturer.Items.Add("Все производители");
                foreach (var m in context.Manufacturers.AsNoTracking().OrderBy(x => x.Name).ToList())
                    cmbManufacturer.Items.Add(new ComboItem(m.Id, m.Name));
                cmbManufacturer.SelectedIndex = 0;
            }

            // Авто-применение фильтров
            txtSearch.TextChanged += (_, __) => ApplyFilters();
            cmbCategory.SelectedIndexChanged += (_, __) => ApplyFilters();
            cmbManufacturer.SelectedIndexChanged += (_, __) => ApplyFilters();
            txtPriceMin.TextChanged += (_, __) => ApplyFilters();
            txtPriceMax.TextChanged += (_, __) => ApplyFilters();
            chkInStock.CheckedChanged += (_, __) => ApplyFilters();

            btnResetFilters.Click += (_, __) =>
            {
                txtSearch.Text = "";
                cmbCategory.SelectedIndex = 0;
                cmbManufacturer.SelectedIndex = 0;
                txtPriceMin.Text = "";
                txtPriceMax.Text = "";
                chkInStock.Checked = false;
                ApplyFilters();
            };
        }

        private void ApplyFilters()
        {
            flowLayoutPanelProducts.Controls.Clear();

            var search = (txtSearch.Text ?? "").Trim();
            var selectedCategory = cmbCategory.SelectedItem as ComboItem;
            var selectedManufacturer = cmbManufacturer.SelectedItem as ComboItem;

            float? priceMin = null;
            float? priceMax = null;

            if (float.TryParse(txtPriceMin.Text.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var pMin))
                priceMin = pMin;
            if (float.TryParse(txtPriceMax.Text.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var pMax))
                priceMax = pMax;

            using (var context = new ApplicationContext())
            {
                // Базовый запрос
                var query = context.Products
                    .AsNoTracking()
                    .Include(p => p.ProductCategory)
                    .Include(p => p.Manufacturers)
                    .Include(p => p.Suppliers)
                    .Include(p => p.Units)
                    .AsQueryable();

                // Поиск (артикул / название)

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(p =>
                        p.Article.Contains(search) ||
                        p.Name.Contains(search));
                }

                // Категория

                if (selectedCategory != null)
                    query = query.Where(p => p.ProductCategoryId == selectedCategory.Id);

                // Производитель

                if (selectedManufacturer != null)
                    query = query.Where(p => p.ManufacturersId == selectedManufacturer.Id);

                // Цена от/до

                if (priceMin.HasValue)
                    query = query.Where(p => p.Price >= priceMin.Value);

                if (priceMax.HasValue)
                    query = query.Where(p => p.Price <= priceMax.Value);

                // Только в наличии

                if (chkInStock.Checked)
                    query = query.Where(p => p.QuantityInStorage > 0);

                var products = query
                    .OrderBy(p => p.Name)
                    .ToList();

                foreach (var product in products)
                {
                    var productControl = new ProductControl(product);
                    productControl.ProductDeleted += (s, e) => ApplyFilters(); // обновляем после удаления
                    productControl.ProductEdited += (s, e) => ApplyFilters();  // обновляем после редактирования
                    flowLayoutPanelProducts.Controls.Add(productControl);
                }
            }
        }




        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var editForm = new ProductEditForm(null))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilters();
                }
            }
        }
    }
}
