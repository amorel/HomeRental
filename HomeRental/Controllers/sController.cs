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
            Bounds bnds = requestSearchAjax.bounds;
            var rentals = from r in db.Rentals
                            where   r.Latitude < bnds.northEastLatLng.Lat &&
                                    r.Latitude > bnds.southWestLatLng.Lat &&
                                    r.Longitude < bnds.northEastLatLng.Lng &&
                                    r.Longitude > bnds.southWestLatLng.Lng
                            select new RentalView {
                                ID = r.ID,
                                Capacity = r.Capacity,
                                PricePerNight = r.PricePerNight,
                                GroupPhotoId = r.GroupPhotoId,
                                PropertyType = r.PropertyType,
                                Description = r.Description,
                                Address = r.Address,
                                number = r.number,
                                PostalCode = r.PostalCode,
                                City = r.City,
                                Country = r.Country,
                                Latitude = r.Latitude,
                                Longitude = r.Longitude
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
        public ActionResult Create([Bind(Include="ID,Capacity,GroupPhotoId,PropertyType,Description,Address,PostalCode,City,Country,Longitude,Latitude")] Rental rental)
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
        public ActionResult Edit([Bind(Include="ID,Capacity,GroupPhotoId,PropertyType,Description,Address,PostalCode,City,Country,Longitude,Latitude")] Rental rental)
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
