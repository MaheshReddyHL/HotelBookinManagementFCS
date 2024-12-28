﻿using System;
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
    public class HotelRoomCustomersController : Controller
    {
        private HotelRoomBookingEntities1 db = new HotelRoomBookingEntities1();

        // GET: HotelRoomCustomers
        public ActionResult Index()
        {
            return View(db.HotelRooms.ToList());
        }

        // GET: HotelRoomCustomers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRoom hotelRoom = db.HotelRooms.Find(id);
            if (hotelRoom == null)
            {
                return HttpNotFound();
            }
            return View(hotelRoom);
        }

        // GET: HotelRoomCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelRoomCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomID,RoomNumber,RoomType,AvailabilityStatus,Price,Capacity")] HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                db.HotelRooms.Add(hotelRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotelRoom);
        }

        // GET: HotelRoomCustomers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRoom hotelRoom = db.HotelRooms.Find(id);
            if (hotelRoom == null)
            {
                return HttpNotFound();
            }
            return View(hotelRoom);
        }

        // POST: HotelRoomCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomID,RoomNumber,RoomType,AvailabilityStatus,Price,Capacity")] HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelRoom);
        }

        // GET: HotelRoomCustomers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRoom hotelRoom = db.HotelRooms.Find(id);
            if (hotelRoom == null)
            {
                return HttpNotFound();
            }
            return View(hotelRoom);
        }

        // POST: HotelRoomCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelRoom hotelRoom = db.HotelRooms.Find(id);
            db.HotelRooms.Remove(hotelRoom);
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
