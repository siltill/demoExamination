using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoNEW_EFCoreVersion.DatabaseModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateDelivery { get; set; }
        public int PickupPointId { get; set; }
        public int UsersId { get; set; }
        public string ReceiptCode { get; set; }
        public int OrderStatusId { get; set; }

        // Навигационные свойства
        public virtual PickupPoint PickupPoint { get; set; }
        public virtual Users Users { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderContents> OrderContents { get; set; }
    }
}
