﻿using System;
using System.Collections.Generic;
using System.Text;
using GiftShopBusinessLogic.Attributes;
using System.Runtime.Serialization;

namespace GiftShopBusinessLogic.ViewModels
{
    [DataContract]
    public abstract class BaseViewModel
    {
        [Column(visible: false)]
        [DataMember]
        public int Id { get; set; }
        public abstract List<string> Properties();
    }
}
