using System;
using System.Collections.Generic;
using System.Linq;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.ViewModels;
using GiftShopFileImplement.Models;

namespace GiftShopFileImplement.Implements
{
    public class MaterialLogic : IMaterialLogic
    {
        private readonly FileDataListSingleton source;
        public MaterialLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(MaterialBindingModel model)
        {
            Material element = source.Materials.FirstOrDefault(rec => rec.MaterialName == model.MaterialName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Materials.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Materials.Count > 0 ? source.Materials.Max(rec =>
               rec.Id) : 0;
                element = new Material { Id = maxId + 1 };
                source.Materials.Add(element);
            }
            element.MaterialName = model.MaterialName;
        }
        public void Delete(MaterialBindingModel model)
        {
            Material element = source.Materials.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Materials.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<MaterialViewModel> Read(MaterialBindingModel model)
        {
            return source.Materials
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new MaterialViewModel
            {
                Id = rec.Id,
                MaterialName = rec.MaterialName
            })
            .ToList();
        }
    }
}
