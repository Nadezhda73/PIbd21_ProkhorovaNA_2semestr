using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using GiftShopBusinessLogic.Attributes;
using GiftShopBusinessLogic.Enums;

namespace GiftShopBusinessLogic.ViewModels
{
    [DataContract]
    public class GiftSetViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string GiftSetName { get; set; }
        [Column(title: "Цена", width: 100)]
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> GiftSetMaterials { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "GiftSetName",
            "Price"
        };
    }
}
