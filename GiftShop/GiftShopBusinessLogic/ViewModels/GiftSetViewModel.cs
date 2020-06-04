﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GiftShopBusinessLogic.ViewModels
{
    [DataContract]
    public class GiftSetViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название подарочного набора")]

        [DataMember]
        public string GiftSetName { get; set; }
        [DisplayName("Цена")]

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> GiftSetMaterials { get; set; }
    }
}
