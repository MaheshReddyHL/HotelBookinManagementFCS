using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApplication1;
using WebApplication1.Models;
using System.Linq;
using System.Web.Mvc;

namespace HotelBookUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new HotelRoomBookingEntities1())
            {
                var admin = new AdminLogin
                {
                    AdminId = 501,
                    UserName = "User2",
                    Password = "@User3"
                };

                // Act
                context.AdminLogins.Add(admin);
                context.SaveChanges();  // This commits the changes to the database

                // Assert - Check if the data was inserted
                var insertedAdmin = context.AdminLogins.SingleOrDefault(a => a.AdminId == 501);  // Query the database to find the admin by AdminId
                Assert.IsNotNull(insertedAdmin);
                Assert.AreEqual(501, insertedAdmin.AdminId);
                Assert.AreEqual("User2", insertedAdmin.UserName);
                Assert.AreEqual("@User3", insertedAdmin.Password);
            }
        }
    }
    
}
