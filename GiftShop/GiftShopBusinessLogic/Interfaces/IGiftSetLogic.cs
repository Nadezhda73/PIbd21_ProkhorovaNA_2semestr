using System;
using System.Collections.Generic;
using System.Text;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.ViewModels;

namespace GiftShopBusinessLogic.Interfaces
{
    public interface IGiftSetLogic
    {
        List<GiftSetViewModel> Read(GiftSetBindingModel model);
        void CreateOrUpdate(GiftSetBindingModel model);
        void Delete(GiftSetBindingModel model);
    }
}
