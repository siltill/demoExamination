using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoNEW_EFCoreVersion.DatabaseModel
{
    public class OrderContents
    {
        public int Id { get; set; }
        public string ProductId { get; set; } // string, т.к. ссылается на Product.Article
        public int OrderId { get; set; }     // int, т.к. ссылается на Order.Id
        public int Quantity { get; set; }

        // Навигационные свойства
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
