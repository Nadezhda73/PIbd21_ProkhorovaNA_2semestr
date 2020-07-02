using System;
using System.Collections.Generic;
using GiftShopBusinessLogic.Attributes;
using GiftShopBusinessLogic.Enums;

namespace GiftShopBusinessLogic.ViewModels
{
    public class MaterialViewModel : BaseViewModel
    {
        [Column(title: "Название материала", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string MaterialName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "MaterialName"
        };
    }
}
