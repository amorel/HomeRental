namespace HomeRental.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HomeRental.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeRental.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "HomeRental.Models.ApplicationDbContext";
        }

        protected override void Seed(HomeRental.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Rentals.AddOrUpdate(p => p.ID,
               new Rental
               {
                   Capacity = 1,
                   PricePerNight = 80,
                   PropertyType = PropertyType.House,
                   Address = "chemin de putdael",
                   number = 12,
                   PostalCode = 1160,
                   City = "Auderghem",
                   Country = "Belgique",
                   Longitude = null,
                   Latitude = null,
               },
               new Rental
               {
                   Capacity = 2,
                   PricePerNight = 60,
                   PropertyType = PropertyType.Apartment,
                   Address = "rue Auguste Orts",
                   number = 25,
                   PostalCode = 1000,
                   City = "Bruxelles",
                   Country = "Belgique",
                   Longitude = null,
                   Latitude = null,
               },
               new Rental
               {
                   Capacity = 3,
                   PricePerNight = 120,
                   PropertyType = PropertyType.House,
                   Address = "Corniche Verte",
                   number = 9,
                   PostalCode = 1150,
                   City = "Woluwe-Saint-Pierre",
                   Country = "Belgique",
                   Longitude = null,
                   Latitude = null,
               });

        }

    }
}
