using HomeRental.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace HomeRental.DAL
{
    public class ApplicationInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Console.WriteLine("Begin of ApplicationInitializer Class");
            //var manager = new UserManager<ApplicationUser>(
            //    new UserStore<ApplicationUser>(
            //        new ApplicationDbContext()));

            //for (int i = 0; i < 4; i++)
            //{
            //    var user = new ApplicationUser()
            //    {
            //        UserName = string.Format("User{0}", i.ToString())
            //    };
            //    manager.Create(user, string.Format("Password{0}", i.ToString()));
            //}

            var Rentals = new List<Rental>
            {
                new Rental{ID=1,Capacity=2,PricePerNight=80,GroupPhotoId=null,PropertyType=PropertyType.House,Description="Desc1",Address="chemin de putdael",number=12,PostalCode=1160,City="Auderghem",Country="Belgium",Longitude=null, Latitude=null},
                new Rental{ID=2,Capacity=1,PricePerNight=60,GroupPhotoId=null,PropertyType=PropertyType.Apartment,Description="Desc2",Address="rue Auguste Orts",number=25,PostalCode=1000,City="Bruxelles",Country="Belgium",Longitude=null, Latitude=null},
                new Rental{ID=3,Capacity=2,PricePerNight=120,GroupPhotoId=null,PropertyType=PropertyType.House,Description="Desc3",Address="Corniche Verte",number=9,PostalCode=1150,City="Woluwe-Saint-Pierre",Country="Belgium",Longitude=null, Latitude=null},
            };

            Rentals.ForEach(s => context.Rentals.Add(s));
            context.SaveChanges();

            //var Reservations = new List<Reservation>
            //{
            //    new Reservation{ID=1,ApplicationUserId=null,RentalId=1,StartingDate=DateTime.Parse("2014-08-01"),EndDate=DateTime.Parse("2014-08-05")},
            //    new Reservation{ID=2,ApplicationUserId=null,RentalId=2,StartingDate=DateTime.Parse("2014-08-12"),EndDate=DateTime.Parse("2014-20-05")},
            //    new Reservation{ID=3,ApplicationUserId=null,RentalId=3,StartingDate=DateTime.Parse("2014-07-06"),EndDate=DateTime.Parse("2014-07-05")},
            //};
            //Reservations.ForEach(s => context.Reservations.Add(s));
            //context.SaveChanges();

        }
    }
}