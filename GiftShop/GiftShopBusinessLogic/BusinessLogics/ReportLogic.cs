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
        private readonly IGiftSetLogic giftSetLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IGiftSetLogic giftSetLogic, IOrderLogic orderLLogic)
        {
            this.giftSetLogic = giftSetLogic;
            this.orderLogic = orderLLogic;
        }

        public List<ReportGiftSetMaterialViewModel> GetGiftSetMaterial()
        {
            var giftSets = giftSetLogic.Read(null);
            var list = new List<ReportGiftSetMaterialViewModel>();
            foreach (var giftSet in giftSets)
            {
                foreach (var pc in giftSet.GiftSetMaterials)
                {
                    var record = new ReportGiftSetMaterialViewModel
                    {
                        GiftSetName = giftSet.GiftSetName,
                        MaterialName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<(DateTime, List<ReportOrdersViewModel>)> GetOrders(ReportBindingModel model)
        {
            List<(DateTime, List<ReportOrdersViewModel>)> list = new List<(DateTime, List<ReportOrdersViewModel>)>();
            var orders = orderLogic.Read(new OrderBindingModel
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
             });
            List<DateTime> dates = new List<DateTime>();
            foreach (var order in orders)
            {
                if (!dates.Contains(order.DateCreate.Date))
                {
                    dates.Add(order.DateCreate.Date);
                }
            }
            foreach (var date in dates)
            {
                (DateTime, List<ReportOrdersViewModel>) record;
                record.Item2 = new List<ReportOrdersViewModel>();

                record.Item1 = date;

                foreach (var order in orders.Where(rec => rec.DateCreate.Date == date))
                {
                    record.Item2.Add(order);
                }

                list.Add(record);
            }
            return list;
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
                Title = "Заказы",
                Orders = GetOrders(model)
            });
        }

        public void SaveGiftSetMaterialsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список подарочных наборов по материалам",
                GiftSetMaterials = GetGiftSetMaterial()
            });
        }
    }
}
