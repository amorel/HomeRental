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

namespace HomeRental.Controllers
{
    [RoutePrefix("s")]
    public class sController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("LocationInAreaAjax")]
        public JsonResult LocationInAreaAjax(RequestSearchAjax requestSearchAjax)
        {
            //var locations = db.Rentals.Where(l => inBounds(l.position, requestSearchAjax.bounds));
            return Json(requestSearchAjax);
        }

        private bool inBounds(LatLng position, Bounds bounds)
        {
            //var eastBound = point.long < bounds.NE.long;
            //var westBound = point.long > bounds.SW.long;
            //var inLong;

            //if (bounds.NE.long < bounds.SW.long) {
            //    inLong = eastBound || westBound;
            //} else {
            //    inLong = eastBound && westBound;
            //}

            //var inLat = point.lat > bounds.SW.lat && point.lat < bounds.NE.lat;
            //return inLat && inLong;
            return true;
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
