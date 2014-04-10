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
    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //***************************
            //Create Roles and Users
            //***************************
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string admin = "Admin";
            string password = "123456";

            UserManager.Create(new ApplicationUser() { UserName = "user1" }, "password1" );
            UserManager.Create(new ApplicationUser() { UserName = "user2" }, "password2");
            UserManager.Create(new ApplicationUser() { UserName = "user3" }, "password3");

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(admin))
            {
                var roleresult = RoleManager.Create(new IdentityRole(admin));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = admin;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, admin);
            }
            base.Seed(context);

            //***************************
            //Seed Data in Rentals Context
            //***************************
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