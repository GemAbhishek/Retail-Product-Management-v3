using Cart.Controllers;
using Cart.Model;
using Cart.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace CartTesting
{
    public class Tests
    {
        List<CartItem> cartitem;

        [SetUp]
        
        public void Setup()
            {
                cartitem = new List<CartItem>
        {new CartItem{Id=1,UserName ="Test1" , ProductName ="Mobile", Price = 10999,
                Description ="Lenovo", VendorName ="aaaaaa" , DeliveryDate =DateTime.Parse("12-10-2020"), DeliveryCharge=80 }
        };
        }
        

        [Test]
        public void Test1()

        {
            string username = "Test1";
            var mock = new Mock<ICartItemRepo>();
            mock.Setup(x => x.GetCartItem(username)).Returns(cartitem);
            CartController con = new CartController(mock.Object);
            var data = con.Get(username) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);
        }
       
        [Test]
        public void Test2()
        {
            int Id = 1;
            var mock = new Mock<ICartItemRepo>();
            mock.Setup(x => x.DeleteDetail(Id)).Returns(true);
            CartController con = new CartController(mock.Object);
            var data = con.Delete(Id) as ObjectResult;
            Assert.IsTrue( true);
        }
        [Test]
        /*public void Test4()

        {
            string username = "Test1";
            ProductItem prod = new ProductItem { Id = 1, Name = "Mobile", Description = "Android phone", Price = 10999,
                Rating = 5, Image_name = "Mob.jpg", IsAvailable = true };
            VendorDetail ven = new VendorDetail { Id = 1, Name = "Cloud Retail", Rating = 5, DeliveryCharge = 80 };
            var mock = new Mock<ICartItemRepo>();
            mock.Setup(x => x.PostCartItem(username,prod,ven)).Returns(true);
            CartController con = new CartController(mock.Object);
            var data = con.Post(username,prod) as OkObjectResult;
            Assert.IsTrue(true);
        }*/
        public void Test3()
        {
            int Id = 2;
            var mock = new Mock<ICartItemRepo>();
            mock.Setup(x => x.DeleteDetail(Id)).Returns(true);
            CartController con = new CartController(mock.Object);
            var data = con.Delete(Id) as ObjectResult;
            Assert.IsTrue(true);
        }


        }

}