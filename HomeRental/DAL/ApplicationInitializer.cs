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

            UserManager.Create(new ApplicationUser() { UserName = "user1", HashMail = "767fc9c115a1b989744c755db47feb60" }, "password1");
            UserManager.Create(new ApplicationUser() { UserName = "user2", HashMail = "22bd03ace6f176bfe0c593650bcf45d8" }, "password2");
            UserManager.Create(new ApplicationUser() { UserName = "user3", HashMail = "205e460b479e2e5b48aec07710c08d50" }, "password3");
            UserManager.Create(new ApplicationUser() { UserName = "user4", HashMail = "f553c130b11b71282b7c022ca82355e9" }, "password4");

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(admin))
            {
                var roleresult = RoleManager.Create(new IdentityRole(admin));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = admin;
            user.HashMail = "0f748331c0993a973da0847fedc64ba6";
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, admin);
            }
            base.Seed(context);

            //id of the Users
            string user1Id = UserManager.FindByName("user1").Id;
            string user2Id = UserManager.FindByName("user2").Id;
            string user3Id = UserManager.FindByName("user3").Id;
            string user4Id = UserManager.FindByName("user4").Id;
            string user5Id = UserManager.FindByName("Admin").Id;

            //************************************
            //Seed Data in Rentals & Photos Context (Table)
            //************************************
            Rental rental1 = new Rental { ID = 1, Owner = user5Id, Capacity = 2, PricePerNight = 80, PropertyType = PropertyType.House, Description = "Desc1", Address = "chemin de putdael", number = 12, PostalCode = 1160, City = "Auderghem", Country = "Belgium", Latitude = 50.8193813, Longitude = 4.439141500000005 };
            Rental rental2 = new Rental { ID = 2, Owner = user1Id, Capacity = 1, PricePerNight = 60, PropertyType = PropertyType.Apartment, Description = "Desc2", Address = "rue Auguste Orts", number = 25, PostalCode = 1000, City = "Bruxelles", Country = "Belgium", Latitude = 50.8488048, Longitude = 4.3483678000000054 };
            Rental rental3 = new Rental { ID = 3, Owner = user3Id, Capacity = 2, PricePerNight = 120, PropertyType = PropertyType.Chalet, Description = "Desc3", Address = "Corniche Verte", number = 9, PostalCode = 1150, City = "Woluwe-Saint-Pierre", Country = "Belgium", Latitude = 50.8293766, Longitude = 4.467397600000027 };
            Rental rental4 = new Rental { ID = 4, Owner = user4Id, Capacity = 5, PricePerNight = 75, PropertyType = PropertyType.House, Description = "Desc4", Address = "Rue d'Irlande", number = 57, PostalCode = 1060, City = "Sint-Gilles", Country = "Belgium", Latitude = 50.825885, Longitude = 4.351920 };
            Rental rental5 = new Rental { ID = 5, Owner = user2Id, Capacity = 3, PricePerNight = 200, PropertyType = PropertyType.Apartment, Description = "Desc5", Address = "Rue des Champs Elysées", number = 15, PostalCode = 1050, City = "Ixelles", Country = "Belgium", Latitude = 50.831795, Longitude = 4.365922 };

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

            Photo photo20 = new Photo { ID = 20, PathImage = "~/images/4/1.jpg", Rental = rental4 };
            Photo photo21 = new Photo { ID = 21, PathImage = "~/images/4/2.jpg", Rental = rental4 };
            Photo photo22 = new Photo { ID = 22, PathImage = "~/images/4/3.jpg", Rental = rental4 };
            Photo photo23 = new Photo { ID = 23, PathImage = "~/images/4/4.jpg", Rental = rental4 };
            Photo photo24 = new Photo { ID = 24, PathImage = "~/images/4/5.jpg", Rental = rental4 };
            Photo photo25 = new Photo { ID = 25, PathImage = "~/images/4/6.jpg", Rental = rental4 };

            Photo photo26 = new Photo { ID = 26, PathImage = "~/images/5/1.jpg", Rental = rental5 };
            Photo photo27 = new Photo { ID = 27, PathImage = "~/images/5/2.jpg", Rental = rental5 };
            Photo photo28 = new Photo { ID = 28, PathImage = "~/images/5/3.jpg", Rental = rental5 };
            Photo photo29 = new Photo { ID = 29, PathImage = "~/images/5/4.jpg", Rental = rental5 };
            Photo photo30 = new Photo { ID = 30, PathImage = "~/images/5/5.jpg", Rental = rental5 };
            Photo photo31 = new Photo { ID = 31, PathImage = "~/images/5/6.jpg", Rental = rental5 };

            var Photos = new List<Photo>
            {
                photo1,photo2,photo3,photo4,photo5,photo6,photo7,photo8,photo9,photo10,photo11,photo12,photo13,photo14,photo15,photo16,photo17,photo18,photo19,photo20,photo21,photo22,photo23,photo24,photo25,photo26,photo27,photo28,photo29,photo30,photo31
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

            rental4.Photos.Add(photo20);
            rental4.Photos.Add(photo21);
            rental4.Photos.Add(photo22);
            rental4.Photos.Add(photo23);
            rental4.Photos.Add(photo24);
            rental4.Photos.Add(photo25);

            rental5.Photos.Add(photo26);
            rental5.Photos.Add(photo27);
            rental5.Photos.Add(photo28);
            rental5.Photos.Add(photo29);
            rental5.Photos.Add(photo30);
            rental5.Photos.Add(photo31);

            var Rentals = new List<Rental>
            {
                rental1,
                rental2,
                rental3,
                rental4,
                rental5
            };
            Rentals.ForEach(s => context.Rentals.Add(s));

            //***************************************
            //Seed Data in Reservation Context (Table)
            //****************************************

            var Reservations = new List<Reservation>
            {
                new Reservation{ID=1,ApplicationUserId=user1Id,RentalId=1,StartingDate=DateTime.Parse("2014-08-01"),EndDate=DateTime.Parse("2014-08-27")},
                new Reservation{ID=2,ApplicationUserId=user2Id,RentalId=2,StartingDate=DateTime.Parse("2014-09-01"),EndDate=DateTime.Parse("2014-09-27")},
                new Reservation{ID=3,ApplicationUserId=user3Id,RentalId=3,StartingDate=DateTime.Parse("2014-10-01"),EndDate=DateTime.Parse("2014-10-27")},
            };
            Reservations.ForEach(s => context.Reservations.Add(s));


            //***************************************
            //            SAVE CHANGES for ALL
            //****************************************
            context.SaveChanges();

        }
    }
}