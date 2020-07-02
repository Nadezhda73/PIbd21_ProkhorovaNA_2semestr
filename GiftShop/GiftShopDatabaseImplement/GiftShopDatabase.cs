using System;
using GiftShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftShopDatabaseImplement
{
    public class GiftShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1P2ORBM;Initial Catalog=GiftShopShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Material> Materials { set; get; }
        public virtual DbSet<GiftSet> GiftSets { set; get; }
        public virtual DbSet<GiftSetMaterial> GiftSetMaterials { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
        public virtual DbSet<MessageInfo> MessageInfoes { set; get; }
    }
}
