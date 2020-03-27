using System;
using System.Collections.Generic;
using System.Text;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.ViewModels;
using GiftShopListImplement.Models;

namespace GiftShopListImplement.Implements
{
    public class GiftSetLogic : IGiftSetLogic
    {
        private readonly DataListSingleton source;

        public GiftSetLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(GiftSetBindingModel model)
        {
            GiftSet tempGiftSet = model.Id.HasValue ? null : new GiftSet { Id = 1 };
            foreach (var giftSet in source.GiftSets)
            {
                if (giftSet.GiftSetName == model.GiftSetName && giftSet.Id != model.Id)
                {
                    throw new Exception("Уже есть подарочный набор с таким названием");
                }
                if (!model.Id.HasValue && giftSet.Id >= tempGiftSet.Id)
                {
                    tempGiftSet.Id = giftSet.Id + 1;
                }
                else if (model.Id.HasValue && giftSet.Id == model.Id)
                {
                    tempGiftSet = giftSet;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempGiftSet == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempGiftSet);
            }
            else
            {
                source.GiftSets.Add(CreateModel(model, tempGiftSet));
            }
        }
        public void Delete(GiftSetBindingModel model)
        {
            for (int i = 0; i < source.GiftSetMaterials.Count; ++i)
            {
                if (source.GiftSetMaterials[i].GiftSetId == model.Id)
                {
                    source.GiftSetMaterials.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.GiftSets.Count; ++i)
            {
                if (source.GiftSets[i].Id == model.Id)
                {
                    source.GiftSets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private GiftSet CreateModel(GiftSetBindingModel model, GiftSet giftSet)
        {
            giftSet.GiftSetName = model.GiftSetName;
            giftSet.Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.GiftSetMaterials.Count; ++i)
            {
                if (source.GiftSetMaterials[i].Id > maxPCId)
                {
                    maxPCId = source.GiftSetMaterials[i].Id;
                }
                if (source.GiftSetMaterials[i].GiftSetId == giftSet.Id)
                {
                    if
                    (model.GiftSetMaterials.ContainsKey(source.GiftSetMaterials[i].MaterialId))
                    {
                        source.GiftSetMaterials[i].Count =
                        model.GiftSetMaterials[source.GiftSetMaterials[i].MaterialId].Item2;

                        model.GiftSetMaterials.Remove(source.GiftSetMaterials[i].MaterialId);
                    }
                    else
                    {
                        source.GiftSetMaterials.RemoveAt(i--);
                    }
                }
            }
            foreach (var pc in model.GiftSetMaterials)
            {
                source.GiftSetMaterials.Add(new GiftSetMaterial
                {
                    Id = ++maxPCId,
                    GiftSetId = giftSet.Id,
                    MaterialId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return giftSet;
        }
        public List<GiftSetViewModel> Read(GiftSetBindingModel model)
        {
            List<GiftSetViewModel> result = new List<GiftSetViewModel>();
            foreach (var material in source.GiftSets)
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
        private GiftSetViewModel CreateViewModel(GiftSet giftSet)
        {
            Dictionary<int, (string, int)> giftSetMaterials = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.GiftSetMaterials)
            {
                if (pc.GiftSetId == giftSet.Id)
                {
                    string materialName = string.Empty;
                    foreach (var material in source.Materials)
                    {
                        if (pc.MaterialId == material.Id)
                        {
                            materialName = material.MaterialName;
                            break;
                        }
                    }
                    giftSetMaterials.Add(pc.MaterialId, (materialName, pc.Count));
                }
            }
            return new GiftSetViewModel
            {
                Id = giftSet.Id,
                GiftSetName = giftSet.GiftSetName,
                Price = giftSet.Price,
                GiftSetMaterials = giftSetMaterials
            };
        }
    }
}
