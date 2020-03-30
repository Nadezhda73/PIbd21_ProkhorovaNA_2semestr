using System;
using System.Collections.Generic;
using System.Linq;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.ViewModels;
using GiftShopFileImplement.Models;

namespace GiftShopFileImplement.Implements
{
    public class GiftSetLogic : IGiftSetLogic
    {
        private readonly FileDataListSingleton source;
        public GiftSetLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(GiftSetBindingModel model)
        {
            GiftSet element = source.GiftSets.FirstOrDefault(rec => rec.GiftSetName ==
           model.GiftSetName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть подарочный набор с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.GiftSets.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.GiftSets.Count > 0 ? source.Materials.Max(rec =>
               rec.Id) : 0;
                element = new GiftSet { Id = maxId + 1 };
                source.GiftSets.Add(element);
            }
            element.GiftSetName = model.GiftSetName;
            element.Price = model.Price;
            source.GiftSetMaterials.RemoveAll(rec => rec.GiftSetId == model.Id &&
           !model.GiftSetMaterials.ContainsKey(rec.MaterialId));
            var updateMaterials = source.GiftSetMaterials.Where(rec => rec.GiftSetId ==
           model.Id && model.GiftSetMaterials.ContainsKey(rec.MaterialId));
            foreach (var updateMaterial in updateMaterials)
            {
                updateMaterial.Count =
               model.GiftSetMaterials[updateMaterial.MaterialId].Item2;
                model.GiftSetMaterials.Remove(updateMaterial.MaterialId);
            }
            int maxPCId = source.GiftSetMaterials.Count > 0 ?
           source.GiftSetMaterials.Max(rec => rec.Id) : 0;
            foreach (var pc in model.GiftSetMaterials)
            {
                source.GiftSetMaterials.Add(new GiftSetMaterial
                {
                    Id = ++maxPCId,
                    GiftSetId = element.Id,
                    MaterialId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(GiftSetBindingModel model)
        {
            source.GiftSetMaterials.RemoveAll(rec => rec.GiftSetId == model.Id);
            GiftSet element = source.GiftSets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.GiftSets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<GiftSetViewModel> Read(GiftSetBindingModel model)
        {
            return source.GiftSets
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new GiftSetViewModel
            {
                Id = rec.Id,
                GiftSetName = rec.GiftSetName,
                Price = rec.Price,
                GiftSetMaterials = source.GiftSetMaterials
            .Where(recPC => recPC.GiftSetId == rec.Id)
           .ToDictionary(recPC => recPC.MaterialId, recPC =>
            (source.Materials.FirstOrDefault(recC => recC.Id ==
           recPC.MaterialId)?.MaterialName, recPC.Count))
            })
            .ToList();
        }
    }
}
