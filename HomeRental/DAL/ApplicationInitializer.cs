using HomeRental.Models;
using HomeRental.Models.SubModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
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

            //************************************
            //Seed Data in Rentals Context (Table)
            //************************************
            var Rentals = new List<Rental>
            {
                new Rental{ID=1,Capacity=2,PricePerNight=80,GroupPhotoId=null,PropertyType=PropertyType.House,Description="Desc1",Address="chemin de putdael",number=12,PostalCode=1160,City="Auderghem",Country="Belgium", Latitude=50.8193813, Longitude=4.439141500000005},
                new Rental{ID=2,Capacity=1,PricePerNight=60,GroupPhotoId=null,PropertyType=PropertyType.Apartment,Description="Desc2",Address="rue Auguste Orts",number=25,PostalCode=1000,City="Bruxelles",Country="Belgium", Latitude=50.8488048, Longitude=4.3483678000000054},
                new Rental{ID=3,Capacity=2,PricePerNight=120,GroupPhotoId=null,PropertyType=PropertyType.House,Description="Desc3",Address="Corniche Verte",number=9,PostalCode=1150,City="Woluwe-Saint-Pierre",Country="Belgium",Latitude=50.8293766, Longitude=4.467397600000027},
            };
            Rentals.ForEach(s => context.Rentals.Add(s));
            context.SaveChanges();

            //***************************************
            //Seed Data in Reservation Context (Table)
            //****************************************

            string user1Id = UserManager.FindByName("user1").Id;
            string user2Id = UserManager.FindByName("user2").Id;
            string user3Id = UserManager.FindByName("user3").Id;

            var Reservations = new List<Reservation>
            {
                new Reservation{ID=1,ApplicationUserId=user1Id,RentalId=1,StartingDate=DateTime.Parse("2014-01-08"),EndDate=DateTime.Parse("2014-05-08")},
                new Reservation{ID=2,ApplicationUserId=user2Id,RentalId=2,StartingDate=DateTime.Parse("2014-12-08"),EndDate=DateTime.Parse("2014-05-20")},
                new Reservation{ID=3,ApplicationUserId=user3Id,RentalId=3,StartingDate=DateTime.Parse("2014-06-07"),EndDate=DateTime.Parse("2014-05-07")},
            };
            Reservations.ForEach(s => context.Reservations.Add(s));
            context.SaveChanges();

        }
    }
}