using System;
using System.Collections.Generic;
using System.Linq;
using GiftShopDatabaseImplement.Implements;
using GiftShopBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GiftShopBusinessLogic.BusinessLogics;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.ViewModels;
using GiftShopRestApi.Models;

namespace GiftShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IGiftSetLogic _giftSet;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IGiftSetLogic giftSet, MainLogic main)
        {
            _order = order;
            _giftSet = giftSet;
            _main = main;
        }
        [HttpGet]
        public List<GiftSetModel> GetGiftSetList() => _giftSet.Read(null)?.Select(rec =>
       Convert(rec)).ToList();
        [HttpGet]
        public GiftSetModel GetGiftSet(int giftSetId) => Convert(_giftSet.Read(new
       GiftSetBindingModel
        { Id = giftSetId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private GiftSetModel Convert(GiftSetViewModel model)
        {
            if (model == null) return null;
            return new GiftSetModel
            {
                Id = model.Id,
                GiftSetName = model.GiftSetName,
                Price = model.Price
            };
        }
    }
}