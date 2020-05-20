using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShopBusinessLogic.ViewModels;

namespace GiftShopBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<IGrouping<DateTime, ReportOrdersViewModel>> Orders { get; set; }
    }
}
