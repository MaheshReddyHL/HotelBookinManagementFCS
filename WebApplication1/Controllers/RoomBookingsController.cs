using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RoomBookingsController : Controller
    {
        private HotelRoomBookingEntities1 db = new HotelRoomBookingEntities1();

        // GET: RoomBookings
        public ActionResult Index()
        {
            var roomBookings = db.RoomBookings.Include(r => r.CustomerLogin).Include(r => r.HotelRoom);
            return View(roomBookings.ToList());
        }

        // GET: RoomBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.CustomerLogins, "CustomerId", "Name");
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,CustomerID,RoomID,CustomerName,CheckInDate,CheckOutDate,Price,Status")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roomBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.CustomerLogins, "CustomerId", "Name", roomBooking.CustomerID);
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomBooking.RoomID);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.CustomerLogins, "CustomerId", "Name", roomBooking.CustomerID);
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomBooking.RoomID);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,CustomerID,RoomID,CustomerName,CheckInDate,CheckOutDate,Price,Status")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.CustomerLogins, "CustomerId", "Name", roomBooking.CustomerID);
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomBooking.RoomID);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            db.RoomBookings.Remove(roomBooking);
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
