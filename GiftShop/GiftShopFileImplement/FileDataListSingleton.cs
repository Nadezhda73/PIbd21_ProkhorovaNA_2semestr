using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using GiftShopBusinessLogic.Enums;
using GiftShopFileImplement.Models;

namespace GiftShopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string MaterialFileName = "Material.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string GiftSetFileName = "GiftSet.xml";
        private readonly string GiftSetMaterialFileName = "GiftSetMaterial.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";
        public List<Material> Materials { get; set; }
        public List<Order> Orders { get; set; }
        public List<GiftSet> GiftSets { get; set; }
        public List<GiftSetMaterial> GiftSetMaterials { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> MessageInfoes { get; set; }
        private FileDataListSingleton()
        {
            Materials = LoadMaterials();
            Orders = LoadOrders();
            GiftSets = LoadGiftSets();
            GiftSetMaterials = LoadGiftSetMaterials();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessageInfoes = LoadMessageInfoes();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveMaterials();
            SaveOrders();
            SaveGiftSets();
            SaveGiftSetMaterials();
            SaveClients();
            SaveImplementers();
        }
        private List<Material> LoadMaterials()
        {
            var list = new List<Material>();
            if (File.Exists(MaterialFileName))
            {
                XDocument xDocument = XDocument.Load(MaterialFileName);
                var xElements = xDocument.Root.Elements("Material").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Material
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        MaterialName = elem.Element("MaterialName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        GiftSetId = Convert.ToInt32(elem.Element("GiftSetId").Value),
                        ImplementerId = string.IsNullOrEmpty(elem.Element("ImplementerId").Value) ? (int?)null : Convert.ToInt32(elem.Element("ImplementerId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<GiftSet> LoadGiftSets()
        {
            var list = new List<GiftSet>();
            if (File.Exists(GiftSetFileName))
            {
                XDocument xDocument = XDocument.Load(GiftSetFileName);
                var xElements = xDocument.Root.Elements("GiftSet").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new GiftSet
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        GiftSetName = elem.Element("GiftSetName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<GiftSetMaterial> LoadGiftSetMaterials()
        {
            var list = new List<GiftSetMaterial>();
            if (File.Exists(GiftSetMaterialFileName))
            {
                XDocument xDocument = XDocument.Load(GiftSetMaterialFileName);
                var xElements = xDocument.Root.Elements("GiftSetMaterial").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new GiftSetMaterial
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        GiftSetId = Convert.ToInt32(elem.Element("GiftSetId").Value),
                        MaterialId = Convert.ToInt32(elem.Element("MaterialId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }

            return list;
        }
        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();

            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
                    });
                }
            }

            return list;
        }
        private List<MessageInfo> LoadMessageInfoes()
        {
            var list = new List<MessageInfo>();

            if (File.Exists(MessageInfoFileName))
            {
                XDocument xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("MessageInfo").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value
                    });
                }
            }

            return list;
        }
        private void SaveMaterials()
        {
            if (Materials != null)
            {
                var xElement = new XElement("Materials");
                foreach (var material in Materials)
                {
                    xElement.Add(new XElement("Material",
                    new XAttribute("Id", material.Id),
                    new XElement("MaterialName", material.MaterialName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MaterialFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("GiftSetId", order.GiftSetId),
                    new XElement("ImplementerId", order.ImplementerId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveGiftSets()
        {
            if (GiftSets != null)
            {
                var xElement = new XElement("GiftSets");
                foreach (var giftSet in GiftSets)
                {
                    xElement.Add(new XElement("GiftSet",
                    new XAttribute("Id", giftSet.Id),
                    new XElement("GiftSetName", giftSet.GiftSetName),
                    new XElement("Price", giftSet.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(GiftSetFileName);
            }
        }
        private void SaveGiftSetMaterials()
        {
            if (GiftSetMaterials != null)
            {
                var xElement = new XElement("GiftSetMaterials");
                foreach (var giftSetMaterial in GiftSetMaterials)
                {
                    xElement.Add(new XElement("GiftSetMaterial",
                    new XAttribute("Id", giftSetMaterial.Id),
                    new XElement("GiftSetId", giftSetMaterial.GiftSetId),
                    new XElement("MaterialId", giftSetMaterial.MaterialId),
                    new XElement("Count", giftSetMaterial.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(GiftSetMaterialFileName);
            }
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("FIO", client.FIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");

                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        private void SaveMessageInfoes()
        {
            if (MessageInfoes != null)
            {
                var xElement = new XElement("MessageInfoes");

                foreach (var messageInfo in MessageInfoes)
                {
                    xElement.Add(new XElement("MessageInfo",
                    new XAttribute("Id", messageInfo.MessageId),
                    new XElement("ClientId", messageInfo.ClientId),
                    new XElement("SenderName", messageInfo.SenderName),
                    new XElement("DateDelivery", messageInfo.DateDelivery),
                    new XElement("Subject", messageInfo.Subject),
                    new XElement("Body", messageInfo.Body)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MessageInfoFileName);
            }
        }
    }
}
