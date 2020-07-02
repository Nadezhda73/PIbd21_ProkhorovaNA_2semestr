using GiftShopBusinessLogic.Attributes;
using GiftShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace GiftShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }
        [DataMember]
        [Column(title: "Почта", width: 150)]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "FIO",
            "Email"
        };
    }
}
