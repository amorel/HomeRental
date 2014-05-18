using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeRental.Models;
using HomeRental.DAL;
using HomeRental.Models.SubModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HomeRental.Controllers
{
    [RoutePrefix("s")]
    public class sController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("LocationInAreaAjax")]
        public ActionResult AjaxView(RequestSearchAjax requestSearchAjax)
        {
            if (requestSearchAjax.checkin == null)
                if (requestSearchAjax.checkout != null)
                {
                    requestSearchAjax.checkout = requestSearchAjax.checkin;
                }
                else
                {
                    requestSearchAjax.checkin = DateTime.Now;
                }
            if (requestSearchAjax.checkout == null) requestSearchAjax.checkout = DateTime.Now;

            Bounds bnds = requestSearchAjax.bounds;
            var rentals = from rent in db.Rentals
                          where rent.Latitude < bnds.northEastLatLng.Lat &&
                                  rent.Latitude > bnds.southWestLatLng.Lat &&
                                  rent.Longitude < bnds.northEastLatLng.Lng &&
                                  rent.Longitude > bnds.southWestLatLng.Lng &&
                                  rent.Capacity >= requestSearchAjax.guests &&
                                  rent.PricePerNight >= requestSearchAjax.minPrice &&
                                  rent.PricePerNight <= requestSearchAjax.maxPrice &&
                                  rent.Reservations.Where(res => (requestSearchAjax.checkin >= res.StartingDate && requestSearchAjax.checkin <= res.EndDate) ||
                                                                 (requestSearchAjax.checkout >= res.StartingDate && requestSearchAjax.checkout <= res.EndDate) ||
                                                                 (requestSearchAjax.checkin <= res.StartingDate && requestSearchAjax.checkout >= res.EndDate)).Count() == 0
                          select new RentalView
                          {
                              ID = rent.ID,
                              OwnerUser = rent.OwnerUser,
                              Capacity = rent.Capacity,
                              PricePerNight = rent.PricePerNight,
                              PropertyType = rent.PropertyType,
                              Description = rent.Description,
                              Address = rent.Address,
                              number = rent.number,
                              PostalCode = rent.PostalCode,
                              City = rent.City,
                              Country = rent.Country,
                              Latitude = rent.Latitude,
                              Longitude = rent.Longitude,
                              Reservations = rent.Reservations,
                              Photos = rent.Photos
                          };

            return View(rentals);
        }

        // GET: /s/
        [Route("{address?}")]
        public ViewResult Index(cQueryString cquerystring)
        {
            ViewBag.checkinShortDate = cquerystring.getShortDateCheckin();
            ViewBag.checkoutShortDate = cquerystring.getShortDateCheckout();
            ViewBag.guests = cquerystring.guests;
            return View();
        }

        // GET: /s/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: /s/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /s/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Capacity,GroupPhotoId,PropertyType,Description,Address,PostalCode,City,Country,Longitude,Latitude")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rental);
        }

        // GET: /s/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: /s/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Capacity,GroupPhotoId,PropertyType,Description,Address,PostalCode,City,Country,Longitude,Latitude")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rental);
        }

        // GET: /s/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: /s/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
