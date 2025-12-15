using demoNEW_EFCoreVersion.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace demoNEW_EFCoreVersion
{
    internal partial class ProductEditForm : Form
    {
        private Product _product;
        private bool _isNew;
        private readonly string _originalArticle;

        public ProductEditForm(Product product)
        {
            InitializeComponent();
            _product = product ?? new Product();
            _isNew = product == null;
            _originalArticle = product?.Article;
            LoadCombos();
            DisplayProduct();

            // Article является PK. При редактировании существующего товара запрещаем менять PK,
            // иначе EF Core не сможет корректно обновить запись (0 строк изменено / null при перезагрузке).
            if (!_isNew)
            {
                txtArticle.ReadOnly = true;
                txtArticle.TabStop = false;
            }
        }

        private void LoadCombos()
        {
            using (var context = new ApplicationContext())
            {
                cmbCategory.DataSource = context.ProductCategories.ToList();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";

                cmbManufacturer.DataSource = context.Manufacturers.ToList();
                cmbManufacturer.DisplayMember = "Name";
                cmbManufacturer.ValueMember = "Id";

                cmbSupplier.DataSource = context.Suppliers.ToList();
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.ValueMember = "Id";

                cmbUnit.DataSource = context.Units.ToList();
                cmbUnit.DisplayMember = "Name";
                cmbUnit.ValueMember = "Id";
            }
        }

        private void DisplayProduct()
        {
            txtArticle.Text = _product.Article;
            txtName.Text = _product.Name;
            cmbUnit.SelectedValue = _product.UnitsId;
            txtPrice.Text = _product.Price.ToString();
            cmbSupplier.SelectedValue = _product.SuppliersId;
            cmbManufacturer.SelectedValue = _product.ManufacturersId;
            cmbCategory.SelectedValue = _product.ProductCategoryId;
            txtDiscount.Text = _product.Discount.ToString();
            txtQuantity.Text = _product.QuantityInStorage.ToString();
            txtDescription.Text = _product.Description;
            txtPhoto.Text = _product.Photo;
        }

        private void btnBrowsePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Picture"); // Устанавливаем начальную директорию
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPhoto.Text = Path.GetFileName(ofd.FileName); // Сохраняем только имя файла
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация (простая)
            if (string.IsNullOrEmpty(txtArticle.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Заполните обязательные поля: Артикул и Наименование.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Заполняем продукт
            var article = txtArticle.Text;
            var name = txtName.Text;

            if (!float.TryParse(txtPrice.Text, out var price) ||
                !float.TryParse(txtDiscount.Text, out var discount) ||
                !float.TryParse(txtQuantity.Text, out var quantity))
            {
                MessageBox.Show("Проверьте числовые поля: Цена, Скидка, Количество.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new ApplicationContext())
            {
                if (_isNew)
                {
                    _product.Article = article;
                    _product.Name = name;
                    _product.UnitsId = (int)cmbUnit.SelectedValue;
                    _product.Price = price;
                    _product.SuppliersId = (int)cmbSupplier.SelectedValue;
                    _product.ManufacturersId = (int)cmbManufacturer.SelectedValue;
                    _product.ProductCategoryId = (int)cmbCategory.SelectedValue;
                    _product.Discount = discount;
                    _product.QuantityInStorage = quantity;
                    _product.Description = txtDescription.Text;
                    _product.Photo = txtPhoto.Text;

                    context.Products.Add(_product);
                }
                else
                {
                    // Для обновления используем сущность из текущего контекста
                    // (избавляемся от проблем detached-объектов и случайных перезаписей).
                    var dbProduct = context.Products.FirstOrDefault(p => p.Article == _originalArticle);
                    if (dbProduct == null)
                    {
                        MessageBox.Show("Не удалось найти товар для обновления (возможно, он был удалён).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // PK менять нельзя
                    if (!string.Equals(article, _originalArticle, StringComparison.Ordinal))
                    {
                        MessageBox.Show("Нельзя изменить артикул существующего товара (это первичный ключ).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dbProduct.Name = name;
                    dbProduct.UnitsId = (int)cmbUnit.SelectedValue;
                    dbProduct.Price = price;
                    dbProduct.SuppliersId = (int)cmbSupplier.SelectedValue;
                    dbProduct.ManufacturersId = (int)cmbManufacturer.SelectedValue;
                    dbProduct.ProductCategoryId = (int)cmbCategory.SelectedValue;
                    dbProduct.Discount = discount;
                    dbProduct.QuantityInStorage = quantity;
                    dbProduct.Description = txtDescription.Text;
                    dbProduct.Photo = txtPhoto.Text;

                    // Отдаём наружу обновлённый объект, чтобы вызывающая форма могла сразу отрисовать изменения
                    _product = dbProduct;
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
