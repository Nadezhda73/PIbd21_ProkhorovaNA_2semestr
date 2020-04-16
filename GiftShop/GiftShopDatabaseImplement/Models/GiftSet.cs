using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GiftShopDatabaseImplement.Models
{
    public class GiftSet
    {
        public int Id { get; set; }
        [Required]
        public string GiftSetName { get; set; }
        [Required]
        public decimal Price { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<GiftSetMaterial> GiftSetMaterials { get; set; }
    }
}
