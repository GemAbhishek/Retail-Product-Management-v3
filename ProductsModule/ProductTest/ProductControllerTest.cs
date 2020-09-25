using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Product.Controllers;
using Product.Model;
using Product.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProductTest
{
    public class ProductControllerTest
    {
        /*
         * 
         only those function has been tested which are reqiuired as given in project 
        *
         */

        List<ProductItem> product;
        [SetUp]
        public void Setup()
        {
            product = new List<ProductItem>()
            {
                new ProductItem{Id=1,Name="Mobile",Description="Androidphone",Price=10999,Rating=5,Image_name="Mob.jpg",IsAvailable=true},
                new ProductItem{Id=2,Name="Laptop",Description="Lenovo,8GB,1TB",Price=45000,Rating=4,Image_name="Lap.jpg",IsAvailable=true},
                new ProductItem{Id=3,Name="Desktop",Description="HPBrand",Price=25000,Rating=4,Image_name="des.jpg",IsAvailable=false},
                new ProductItem{Id=4,Name="AC",Description="Voltas ,1.5Top",Price=32000,Rating=5,Image_name="ac.jpg",IsAvailable=true},
                new ProductItem{Id=5,Name="Heater",Description="Prestige,1KW",Price=1400,Rating=4,Image_name="heater.jpg",IsAvailable=false}

            };
        }

        [Test]
        public void Test1_getall()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetails()).Returns(product);
            ProductController obj = new ProductController(mock.Object);
            var res = obj.Get() as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void Test2_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(1)).Returns(new ProductItem { Id = 1, Name = "Mobile", Description = "Androidphone", Price = 10999, Rating = 5, Image_name = "Mob.jpg", IsAvailable = true });
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("1") as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void Test3_getidName()
        {
            var mock = new Mock<IProductRepo>();
            //Here Repo is Blocked by mock .. means product controller calls repo but data is returned by mock.
            //so what data should be returned is defined below....
            mock.Setup(x => x.GetDetailbyId(999)).Returns((product.Where(x => x.Id == 999)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("999") as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }




        [Test]
        public void Test4_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(55)).Returns((product.Where(x => x.Id == 55)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("55") as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }
        [Test]
        public void Test5_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(11)).Returns((product.Where(x => x.Id == 11)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("11") as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }
        [Test]
        public void Test6_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(3)).Returns((product.Where(x => x.Id == 3)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("3") as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
        [Test]
        public void Test7_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(5)).Returns((product.Where(x => x.Id == 5)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("5") as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
        [Test]
        public void Test8_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(3)).Returns((product.Where(x => x.Id == 3)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("3") as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
        [Test]
        public void Test9_getidName()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.GetDetailbyId(88)).Returns((product.Where(x => x.Id == 88)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var res = obj.GetbyNameOrId("88") as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }

    }
}