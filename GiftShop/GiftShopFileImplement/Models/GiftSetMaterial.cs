using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShopFileImplement.Models
{
    public class GiftSetMaterial
    {
        public int Id { get; set; }
        public int GiftSetId { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
    }
}
