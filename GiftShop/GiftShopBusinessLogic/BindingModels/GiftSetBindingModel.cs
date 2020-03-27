﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShopBusinessLogic.BindingModels
{
    public class GiftSetBindingModel
    {
        public int? Id { get; set; }

        public string GiftSetName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> GiftSetMaterials { get; set; }
    }
}
