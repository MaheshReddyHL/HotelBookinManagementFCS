using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        private  readonly HotelRoomBookingEntities1 db = new HotelRoomBookingEntities1();
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult CustomerDashboard()
        {
            return View();
        }
        

  

       

        // GET: Booking/Create
        [HttpGet]
        public ActionResult Booking()
        {
            // You can define room types and prices here, or fetch from a database if required.
            ViewBag.RoomTypes = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Text = "Single Room ", Value = "Single", Selected = true },
            new SelectListItem { Text = "Double Room ", Value = "Double" },
            new SelectListItem { Text = "Suite Room ", Value = "Suite" },
            new SelectListItem { Text = "Deluxe Room", Value = "Deluxe" }
             }, "Value", "Text");

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Booking(Booking bookingData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save the booking data to the database
                    db.Bookings.Add(bookingData);
                    int resultValue = db.SaveChanges();
                    if (resultValue > 0)
                    {
                        ViewBag.Message = "Booking added successfully";
                        return RedirectToAction("Payment");
                    }
                    else
                    {
                        ViewBag.Message = "Booking not inserted";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            // Reload the room types in case of error to display the form again
            ViewBag.RoomTypes = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Text = "Single Room", Value = "Single" },
            new SelectListItem { Text = "Double Room", Value = "Double" },
            new SelectListItem { Text = " Suite Room", Value = "Single" },
            new SelectListItem { Text = "Deluxe Room", Value = "Double" }
        }, "Value", "Text");

            return View(bookingData);
            
        }

        // Action to get price based on room type
        [HttpPost]
        public JsonResult GetPrice(string roomType)
        {
            decimal Price = 0;

            switch (roomType)
            {
                case "Single":
                    Price = 500; // price for Single Bedroom
                    break;
                case "Double":
                    Price = 600; // price for Double Bedroom
                    break;
                case "Suite":
                    Price = 800;
                    break;
                case "Deluxe":
                    Price = 1000;
                    break;
            }

            return Json(new { price = Price }, JsonRequestBehavior.AllowGet);
        }

        //booking view
      
        [HttpGet]
        public ActionResult BookingView()
        {
            try
            {
                using (var context = new HotelRoomBookingEntities1())
                {
                    var addresses = context.Bookings.ToList(); // Assuming you have an Addresses DbSet
                    return View(addresses);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Response.Write(ex.Message);
                return View(new List<Booking>()); // Return an empty list in case of error
            }
        }
        //delete
        // GET: Delete booking confirmation page by ID
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var context = new HotelRoomBookingEntities1())
            {
                // Find the booking by ID
                var booking = context.Bookings.FirstOrDefault(b => b.BookingID == id);
                if (booking != null)
                {
                    return View(booking); // Pass the booking to the Delete confirmation view
                }
            }
            return HttpNotFound(); // Return 404 if the booking is not found
        }

        // POST: Confirm the deletion of the booking
        [HttpPost, ActionName("Delete")] // This allows the form to use POST to confirm the delete
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (var context = new HotelRoomBookingEntities1())
                {
                    // Find the booking by ID
                    var booking = context.Bookings.FirstOrDefault(b => b.BookingID == id);
                    if (booking != null)
                    {
                        // Remove the booking from the database
                        context.Bookings.Remove(booking);
                        context.SaveChanges(); // Save changes to the database
                    }
                }
                return RedirectToAction("bookingview"); // Redirect to the view after deletion
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Response.Write(ex.Message);
                return RedirectToAction("bookingview"); // Return to view on error
            }
        }
        //edit

        // GET: Edit booking by ID
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var context = new HotelRoomBookingEntities1())
            {
                // Find the booking by ID
                var booking = context.Bookings.FirstOrDefault(b => b.BookingID == id);
                if (booking != null)
                {
                    return View(booking); // Pass the booking to the view
                }
            }
            return HttpNotFound(); // Return 404 if the booking is not found
        }

        // POST: Save updated booking
        [HttpPost]
        public ActionResult Edit(Booking updatedBooking)
        {
            try
            {
                using (var context = new HotelRoomBookingEntities1())
                {
                    // Find the existing booking
                    var existingBooking = context.Bookings.FirstOrDefault(b => b.BookingID == updatedBooking.BookingID);
                    if (existingBooking != null)
                    {
                        // Update the booking properties
                        existingBooking.Customername = updatedBooking.Customername;
                        existingBooking.Phonenumber = updatedBooking.Phonenumber;
                        existingBooking.EmailId = updatedBooking.EmailId;
                        existingBooking.Roomtype = updatedBooking.Roomtype;
                        existingBooking.CheckInDate = updatedBooking.CheckInDate;
                        existingBooking.CheckOutDate = updatedBooking.CheckOutDate;
                        existingBooking.TotalCharge = updatedBooking.TotalCharge;

                        context.SaveChanges(); // Save changes to the database
                    }
                }
                return RedirectToAction("bookingview"); // Redirect to the view after update
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Response.Write(ex.Message);
                return View(updatedBooking); // Return the view with the booking in case of error
            }
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult Sucess()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }



}
