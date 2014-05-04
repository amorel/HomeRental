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
            //Seed Data in Rentals & Photos Context (Table)
            //************************************
            Rental rental1 = new Rental{ID=1,Capacity=2,PricePerNight=80,PropertyType=PropertyType.House,Description="Desc1",Address="chemin de putdael",number=12,PostalCode=1160,City="Auderghem",Country="Belgium",Latitude=50.8193813,Longitude=4.439141500000005};
            Rental rental2 = new Rental{ID=2,Capacity=1,PricePerNight=60,PropertyType=PropertyType.Apartment,Description="Desc2",Address="rue Auguste Orts",number=25,PostalCode=1000,City="Bruxelles",Country="Belgium",Latitude=50.8488048,Longitude=4.3483678000000054};
            Rental rental3 = new Rental{ID=3,Capacity=2,PricePerNight=120,PropertyType=PropertyType.House,Description="Desc3",Address="Corniche Verte",number=9,PostalCode=1150,City="Woluwe-Saint-Pierre",Country="Belgium",Latitude=50.8293766,Longitude=4.467397600000027};

            Photo photo1 = new Photo{ID=1,PathImage="~/images/1/1.jpg",Rental=rental1};
            Photo photo2 = new Photo{ID=2,PathImage="~/images/1/2.jpg",Rental=rental1};
            Photo photo3 = new Photo{ID=3,PathImage="~/images/1/3.jpg",Rental=rental1};
            Photo photo4 = new Photo{ID=4,PathImage="~/images/1/4.jpg",Rental=rental1};
            Photo photo5 = new Photo{ID=5,PathImage="~/images/1/5.jpg",Rental=rental1};
            Photo photo6 = new Photo{ID=6,PathImage="~/images/1/6.jpg",Rental=rental1};
            Photo photo7 = new Photo{ID=7,PathImage="~/images/1/7.jpg",Rental=rental1};

            Photo photo8 = new Photo{ID=8,PathImage="~/images/2/1.jpg",Rental=rental2};
            Photo photo9 = new Photo{ID=9,PathImage="~/images/2/2.jpg",Rental=rental2};
            Photo photo10 = new Photo{ID=10,PathImage="~/images/2/3.jpg",Rental=rental2};
            Photo photo11 = new Photo{ID=11,PathImage="~/images/2/4.jpg",Rental=rental2};
            Photo photo12 = new Photo{ID=12,PathImage="~/images/2/5.jpg",Rental=rental2};
            Photo photo13 = new Photo{ID=13,PathImage="~/images/2/6.jpg",Rental=rental2};

            Photo photo14 = new Photo{ID=14,PathImage="~/images/3/1.jpg",Rental=rental3};
            Photo photo15 = new Photo{ID=15,PathImage="~/images/3/2.jpg",Rental=rental3};
            Photo photo16 = new Photo{ID=16,PathImage="~/images/3/3.jpg",Rental=rental3};
            Photo photo17 = new Photo{ID=17,PathImage="~/images/3/4.jpg",Rental=rental3};
            Photo photo18 = new Photo{ID=18,PathImage="~/images/3/5.jpg",Rental=rental3};
            Photo photo19 = new Photo{ID=19,PathImage="~/images/3/6.jpg",Rental=rental3};

            //Photo photo20 = new Photo{ID=20,PathImage="~/images/4/1.jpg"};
            //Photo photo21 = new Photo{ID=21,PathImage="~/images/4/2.jpg"};
            //Photo photo22 = new Photo{ID=22,PathImage="~/images/4/3.jpg"};
            //Photo photo23 = new Photo{ID=23,PathImage="~/images/4/4.jpg"};
            //Photo photo24 = new Photo{ID=24,PathImage="~/images/4/5.jpg"};
            //Photo photo25 = new Photo{ID=25,PathImage="~/images/4/6.jpg"};

            var Photos = new List<Photo>
            {
                photo1,photo2,photo3,photo4,photo5,photo6,photo7,photo8,photo9,photo10,photo11,photo12,photo13,photo14,photo15,photo16,photo17,photo18,photo19
            };
            Photos.ForEach(s => context.Photos.Add(s));

            rental1.Photos.Add(photo1);
            rental1.Photos.Add(photo2);
            rental1.Photos.Add(photo3);
            rental1.Photos.Add(photo4);
            rental1.Photos.Add(photo5);
            rental1.Photos.Add(photo6);
            rental1.Photos.Add(photo7);

            rental2.Photos.Add(photo8);
            rental2.Photos.Add(photo9);
            rental2.Photos.Add(photo10);
            rental2.Photos.Add(photo11);
            rental2.Photos.Add(photo12);
            rental2.Photos.Add(photo13);

            rental3.Photos.Add(photo14);
            rental3.Photos.Add(photo15);
            rental3.Photos.Add(photo16);
            rental3.Photos.Add(photo17);
            rental3.Photos.Add(photo18);
            rental3.Photos.Add(photo19);

            var Rentals = new List<Rental>
            {
                rental1,
                rental2,
                rental3
            };
            Rentals.ForEach(s => context.Rentals.Add(s));

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


            //***************************************
            //            SAVE CHANGES for ALL
            //****************************************
            context.SaveChanges();

        }
    }
}