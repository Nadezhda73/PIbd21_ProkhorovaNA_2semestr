﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShopBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int GiftSetId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
