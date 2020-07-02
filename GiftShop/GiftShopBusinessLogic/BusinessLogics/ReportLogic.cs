using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.HelperModels;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.ViewModels;

namespace GiftShopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IMaterialLogic materialLogic;
        private readonly IGiftSetLogic giftSetLogic;
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IGiftSetLogic giftSetLogic, IMaterialLogic materialLogic, IOrderLogic orderLogic)
        {
            this.giftSetLogic = giftSetLogic;
            this.materialLogic = materialLogic;
            this.orderLogic = orderLogic;
        }

        public List<ReportGiftSetMaterialViewModel> GetGiftSetMaterial()
        {
            var GiftSets = giftSetLogic.Read(null);
            var list = new List<ReportGiftSetMaterialViewModel>();
            foreach (var giftSet in GiftSets)
            {
                foreach (var gm in giftSet.GiftSetMaterials)
                {
                    var record = new ReportGiftSetMaterialViewModel
                    {
                        GiftSetName = giftSet.GiftSetName,
                        MaterialName = gm.Value.Item1,
                        Count = gm.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                GiftSetName = x.GiftSetName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .GroupBy(x => x.DateCreate.Date)
           .ToList();
        }

        public void SaveGiftSetsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список подарочных наборов",
                GiftSets = giftSetLogic.Read(null)
            });
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        public void SaveGiftSetMaterialsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список подарочных наборов по материалам",
                GiftSetMaterials = GetGiftSetMaterial(),
            });
        }
    }
}