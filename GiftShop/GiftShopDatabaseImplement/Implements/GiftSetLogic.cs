using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.ViewModels;
using GiftShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftShopDatabaseImplement.Implements
{
    public class GiftSetLogic : IGiftSetLogic
    {
        public void CreateOrUpdate(GiftSetBindingModel model)
        {
            using (var context = new GiftShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        GiftSet element = context.GiftSets.FirstOrDefault(rec =>
                       rec.GiftSetName == model.GiftSetName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть подарочный набор с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.GiftSets.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new GiftSet();
                            context.GiftSets.Add(element);
                        }
                        element.GiftSetName = model.GiftSetName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var giftSetMaterials = context.GiftSetMaterials.Where(rec
                           => rec.GiftSetId == model.Id.Value).ToList();

                            context.GiftSetMaterials.RemoveRange(giftSetMaterials.Where(rec =>
                            !model.GiftSetMaterials.ContainsKey(rec.MaterialId)).ToList());
                            context.SaveChanges();
                            foreach (var updateMaterial in giftSetMaterials)
                            {
                                updateMaterial.Count =
                               model.GiftSetMaterials[updateMaterial.MaterialId].Item2;

                                model.GiftSetMaterials.Remove(updateMaterial.MaterialId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.GiftSetMaterials)
                        {
                            context.GiftSetMaterials.Add(new GiftSetMaterial
                            {
                                GiftSetId = element.Id,
                                MaterialId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(GiftSetBindingModel model)
        {
            using (var context = new GiftShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.GiftSetMaterials.RemoveRange(context.GiftSetMaterials.Where(rec =>
                        rec.GiftSetId == model.Id));
                        GiftSet element = context.GiftSets.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.GiftSets.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<GiftSetViewModel> Read(GiftSetBindingModel model)
        {
            using (var context = new GiftShopDatabase())
            {
                return context.GiftSets
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new GiftSetViewModel
               {
                   Id = rec.Id,
                   GiftSetName = rec.GiftSetName,
                   Price = rec.Price,
                   GiftSetMaterials = context.GiftSetMaterials.Include(recPC => recPC.Material)
               .Where(recPC => recPC.GiftSetId == rec.Id)
               .ToDictionary(recPC => recPC.MaterialId, recPC =>
                (recPC.Material?.MaterialName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
