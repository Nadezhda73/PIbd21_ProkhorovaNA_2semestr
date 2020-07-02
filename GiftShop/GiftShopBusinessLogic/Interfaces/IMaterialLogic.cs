using System;
using System.Collections.Generic;
using System.Text;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.ViewModels;

namespace GiftShopBusinessLogic.Interfaces
{
    public interface IMaterialLogic
    {
        List<MaterialViewModel> Read(MaterialBindingModel model);
        void CreateOrUpdate(MaterialBindingModel model);
        void Delete(MaterialBindingModel model);
    }
}
