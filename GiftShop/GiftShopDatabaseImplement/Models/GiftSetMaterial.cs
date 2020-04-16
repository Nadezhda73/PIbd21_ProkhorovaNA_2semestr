using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GiftShopDatabaseImplement.Models
{
    public class GiftSetMaterial
    {
        public int Id { get; set; }
        public int GiftSetId { get; set; }
        public int MaterialId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Material Material { get; set; }
        public virtual GiftSet GiftSet { get; set; }
    }
}
