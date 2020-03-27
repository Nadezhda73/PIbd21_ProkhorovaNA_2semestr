using System;
using System.Collections.Generic;
using System.Text;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.ViewModels;
using GiftShopListImplement.Models;

namespace GiftShopListImplement.Implements
{
    public class MaterialLogic : IMaterialLogic
    {
        private readonly DataListSingleton source;
        public MaterialLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(MaterialBindingModel model)
        {
            Material tempMaterial = model.Id.HasValue ? null : new Material
            {
                Id = 1
            };
            foreach (var material in source.Materials)
            {
                if (material.MaterialName == model.MaterialName && material.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть материал с таким названием");
                }
                if (!model.Id.HasValue && material.Id >= tempMaterial.Id)
                {
                    tempMaterial.Id = material.Id + 1;
                }
                else if (model.Id.HasValue && material.Id == model.Id)
                {
                    tempMaterial = material;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempMaterial == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempMaterial);
            }
            else
            {
                source.Materials.Add(CreateModel(model, tempMaterial));
            }
        }
        public void Delete(MaterialBindingModel model)
        {
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                if (source.Materials[i].Id == model.Id.Value)
                {
                    source.Materials.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<MaterialViewModel> Read(MaterialBindingModel model)
        {
            List<MaterialViewModel> result = new List<MaterialViewModel>();
            foreach (var material in source.Materials)
            {
                if (model != null)
                {
                    if (material.Id == model.Id)
                    {
                        result.Add(CreateViewModel(material));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(material));
            }
            return result;
        }
        private Material CreateModel(MaterialBindingModel model, Material material)
        {
            material.MaterialName = model.MaterialName;
            return material;
        }
        private MaterialViewModel CreateViewModel(Material material)
        {
            return new MaterialViewModel
            {
                Id = material.Id,
                MaterialName = material.MaterialName
            };
        }
    }
}
