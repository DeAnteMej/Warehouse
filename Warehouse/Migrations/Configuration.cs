namespace Warehouse.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Warehouse.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Warehouse.Models.WarehouseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Warehouse.Models.WarehouseContext";
        }

        protected override void Seed(Warehouse.Models.WarehouseContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            db.Products.AddOrUpdate(
                p => p.Name,
                new Product {Name="Toaster 2010", Price = 1399, Quantity = 12, Category = "appliances" },
                new Product {Name="Waffler NG", Price = 139, Quantity = 19, Category = "appliances" },
                new Product {Name="Trilobite 2", Price=2399,Quantity=120,Category="appliances"}
                );
        }
    }
}
