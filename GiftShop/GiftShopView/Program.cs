using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using GiftShopFileImplement.Implements;
using GiftShopBusinessLogic.Interfaces;
using GiftShopBusinessLogic.BusinessLogics;

namespace GiftShopView
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            {
                var currentContainer = new UnityContainer();
                currentContainer.RegisterType<IMaterialLogic, MaterialLogic>(new
               HierarchicalLifetimeManager());
                currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
               HierarchicalLifetimeManager());
                currentContainer.RegisterType<IGiftSetLogic, GiftSetLogic>(new
               HierarchicalLifetimeManager());
                currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());
                return currentContainer;
            }
        }
    }
}
