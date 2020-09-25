using NUnit.Framework;
using ProceedToBuy.Controllers;

using ProceedToBuy.Provider;

using Microsoft.AspNetCore.Mvc;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using Product.Model;
using Product.Repository;

namespace NUnitTestProject1
{
    public class Tests
    {
       List <ProductItem> productItems;
        [SetUp]
        public void Setup()
        {
            
            productItems = new List<ProductItem>
             {
              new ProductItem{Id=1,Name="Mobile",Description="Android phone",Price=10999,Rating=5,Image_name="Mob.jpg",IsAvailable=true},
              new ProductItem{Id=2,Name="Laptop",Description="Lenovo, 8GB, 1TB",Price=45000,Rating=4,Image_name="Lap.jpg",IsAvailable=true},
              new ProductItem{Id=3,Name="Desktop",Description="HP Brand",Price=25000,Rating=4,Image_name="des.jpg",IsAvailable=false},
              new ProductItem{Id=4,Name="AC",Description="Voltas , 1.5 Top",Price=32000,Rating=5,Image_name="ac.jpg",IsAvailable=false}

             };


        }

        [Test]
        public  virtual void Test1()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.Get()).Returns(productItems);

            ProceedToBuyProvider prov = new ProceedToBuyProvider(mock.Object);
            ProceedToBuyController pc = new ProceedToBuyController(prov);


            var data = pc.GetbyNameOrId(1);
            var result = data as ObjectResult;

            
            Assert.AreEqual(200,result.StatusCode);


            
        }
        [Test]
        public virtual void Test2()
        {
            var mock = new Mock<IProductRepo>();
            mock.Setup(x => x.Get()).Returns(productItems);

            ProceedToBuyProvider prov = new ProceedToBuyProvider(mock.Object);
            ProceedToBuyController pc = new ProceedToBuyController(prov);


            var data = pc.GetbyNameOrId(100);
            var result = data as ObjectResult;


            Assert.AreEqual(400, result.StatusCode);



        }
    }
}