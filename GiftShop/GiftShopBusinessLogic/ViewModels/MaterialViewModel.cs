using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GiftShopBusinessLogic.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название материала")]
        public string MaterialName { get; set; }
    }
}
