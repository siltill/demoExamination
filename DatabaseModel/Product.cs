using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoNEW_EFCoreVersion.DatabaseModel
{
    public class Product
    {
        [Key] // <-- Указываем, что Article - это первичный ключ
        public string Article { get; set; }

        public string Name { get; set; }
        public int UnitsId { get; set; }
        public float Price { get; set; }
        public int SuppliersId { get; set; }
        public int ManufacturersId { get; set; }
        public int ProductCategoryId { get; set; }
        public float Discount { get; set; }
        public float QuantityInStorage { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        // Навигационные свойства
        public virtual Units Units { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        public virtual Manufacturers Manufacturers { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<OrderContents> OrderContents { get; set; }
    }
}
