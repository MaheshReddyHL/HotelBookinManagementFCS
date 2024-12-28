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
    public class RoomChargesController : Controller
    {
        private HotelRoomBookingEntities1 db = new HotelRoomBookingEntities1();

        // GET: RoomCharges
        public ActionResult Index()
        {
            var roomCharges = db.RoomCharges.Include(r => r.HotelRoom).Include(r => r.RoomService);
            return View(roomCharges.ToList());
        }

        // GET: RoomCharges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCharge roomCharge = db.RoomCharges.Find(id);
            if (roomCharge == null)
            {
                return HttpNotFound();
            }
            return View(roomCharge);
        }

        // GET: RoomCharges/Create
        public ActionResult Create()
        {
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber");
            ViewBag.ServiceID = new SelectList(db.RoomServices, "ServiceID", "ServiceName");
            return View();
        }

        // POST: RoomCharges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChargeID,RoomID,ServiceID,TotalCharge")] RoomCharge roomCharge)
        {
            if (ModelState.IsValid)
            {
                db.RoomCharges.Add(roomCharge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomCharge.RoomID);
            ViewBag.ServiceID = new SelectList(db.RoomServices, "ServiceID", "ServiceName", roomCharge.ServiceID);
            return View(roomCharge);
        }

        // GET: RoomCharges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCharge roomCharge = db.RoomCharges.Find(id);
            if (roomCharge == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomCharge.RoomID);
            ViewBag.ServiceID = new SelectList(db.RoomServices, "ServiceID", "ServiceName", roomCharge.ServiceID);
            return View(roomCharge);
        }

        // POST: RoomCharges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChargeID,RoomID,ServiceID,TotalCharge")] RoomCharge roomCharge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomCharge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomID = new SelectList(db.HotelRooms, "RoomID", "RoomNumber", roomCharge.RoomID);
            ViewBag.ServiceID = new SelectList(db.RoomServices, "ServiceID", "ServiceName", roomCharge.ServiceID);
            return View(roomCharge);
        }

        // GET: RoomCharges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCharge roomCharge = db.RoomCharges.Find(id);
            if (roomCharge == null)
            {
                return HttpNotFound();
            }
            return View(roomCharge);
        }

        // POST: RoomCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomCharge roomCharge = db.RoomCharges.Find(id);
            db.RoomCharges.Remove(roomCharge);
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
