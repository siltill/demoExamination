using demoNEW_EFCoreVersion.DatabaseModel;
using Microsoft.EntityFrameworkCore;
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

namespace demoNEW_EFCoreVersion
{
    internal partial class ProductControl : UserControl
    {
        private Product _product;

        // События для обновления списка в форме
        public event EventHandler ProductEdited;
        public event EventHandler ProductDeleted;

        public ProductControl(Product product)
        {
            InitializeComponent();
            _product = product;
            DisplayProduct();

            // Проверяем роль для показа кнопок редактирования/удаления
            if (UserSession.Role != "Администратор")
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void DisplayProduct()
        {
            // Отображение основной информации
            lblCategoryName.Text = $"{_product.ProductCategory?.Name} | {_product.Name}";
            lblDescription.Text = $"Описание товара: {_product.Description}";
            lblManufacturer.Text = $"Производитель: {_product.Manufacturers?.Name}";
            lblSupplier.Text = $"Поставщик: {_product.Suppliers?.Name}";
            lblUnit.Text = $"Единица измерения: {_product.Units?.Name}";
            lblQuantity.Text = $"Количество на складе: {_product.QuantityInStorage}";
            lblDiscount.Text = $"Действующая скидка: {_product.Discount}%";

            // ==================== НАЧАЛО НОВОЙ ЛОГИКИ ФОРМАТИРОВАНИЯ ====================

            // 1. Сначала сбрасываем все стили к значениям по умолчанию
            this.BackColor = Color.White; // Стандартный фон
            lblPrice.ForeColor = SystemColors.ControlText; // Стандартный цвет текста
            lblPrice.Font = new Font(lblPrice.Font, FontStyle.Regular);
            lblPrice.Text = $"Цена: {_product.Price:F2}";

            // Удаляем динамически созданный Label для новой цены, если он остался от предыдущего товара
            var existingFinalPriceLabel = this.Controls.Find("lblFinalPrice", false).FirstOrDefault();
            if (existingFinalPriceLabel != null)
            {
                this.Controls.Remove(existingFinalPriceLabel);
                existingFinalPriceLabel.Dispose();
            }

            // 2. Форматирование цены при наличии скидки
            if (_product.Discount > 0)
            {
                // Рассчитываем итоговую цену
                decimal finalPrice = (decimal)_product.Price * (1 - ((decimal)_product.Discount / 100m));

                // Основную цену делаем перечеркнутой и красной
                lblPrice.Text = _product.Price.ToString("F2");
                lblPrice.ForeColor = Color.Red;
                lblPrice.Font = new Font(lblPrice.Font, FontStyle.Strikeout);

                // Создаем новый Label для итоговой цены, так как один Label не может иметь разное форматирование
                Label finalPriceLabel = new Label
                {
                    Name = "lblFinalPrice", // Имя для последующего поиска и удаления
                    Text = finalPrice.ToString("F2"),
                    ForeColor = Color.Black,
                    Font = new Font(lblPrice.Font, FontStyle.Bold), // Делаем жирным для акцента
                    AutoSize = true,
                    // Располагаем его рядом со старой ценой
                    Location = new Point(lblPrice.Right + 5, lblPrice.Top)
                };

                this.Controls.Add(finalPriceLabel); // Добавляем новый Label на контрол
            }

            // 3. Выделение фона цветом, если скидка > 15%
            if (_product.Discount > 15)
            {
                this.BackColor = ColorTranslator.FromHtml("#2E8B57"); // Цвет SeaGreen
            }

            // 4. Выделение фона голубым, если товара нет на складе (это условие важнее)
            if (_product.QuantityInStorage == 0)
            {
                this.BackColor = Color.LightBlue;
            }

            // ==================== КОНЕЦ НОВОЙ ЛОГИКИ ФОРМАТИРОВАНИЯ ====================

            // Формируем полный путь к файлу изображения
            if (!string.IsNullOrEmpty(_product.Photo))
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Picture", _product.Photo);

                if (File.Exists(imagePath))
                {
                    pictureBoxPhoto.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBoxPhoto.Image = null;
                    pictureBoxPhoto.Text = "Файл не найден";
                }
            }
            else
            {
                pictureBoxPhoto.Image = null;
                pictureBoxPhoto.Text = "Фото отсутствует";
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var editForm = new ProductEditForm(_product))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Перезагружаем данные продукта
                    using (var context = new ApplicationContext())
                    {
                        _product = context.Products
                            .Include(p => p.ProductCategory)
                            .Include(p => p.Manufacturers)
                            .Include(p => p.Suppliers)
                            .Include(p => p.Units)
                            .FirstOrDefault(p => p.Article == _product.Article);
                        DisplayProduct(); // Повторно применяем форматирование
                    }

                    // Сигналим форме, что товар обновился
                    ProductEdited?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var context = new ApplicationContext())
                {
                    var productToDelete = context.Products.FirstOrDefault(p => p.Article == _product.Article);
                    if (productToDelete != null)
                    {
                        context.Products.Remove(productToDelete);
                        context.SaveChanges();
                        this.Parent.Controls.Remove(this); // Удаляем контроль из FlowLayoutPanel

                        // Сигналим форме, что товар удалён
                        ProductDeleted?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void ProductControl_Load(object sender, EventArgs e)
        {

        }
    }
}