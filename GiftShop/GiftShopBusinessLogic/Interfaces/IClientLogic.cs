using System;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace GiftShopBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}