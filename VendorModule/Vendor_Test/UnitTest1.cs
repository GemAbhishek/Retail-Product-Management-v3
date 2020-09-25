using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Vendor.Controllers;
using Vendor.Model;
using Vendor.Repository;

namespace Vendor_Test
{
    public class Tests
    {
        List<VendorDetail> ls = new List<VendorDetail>();
        [SetUp]
        public void Setup()
        {   
            ls = new List<VendorDetail>()
        {
            new VendorDetail{Id=1,Name="Cloud Retail",Rating=5,DeliveryCharge=80 },
            new VendorDetail{Id=2,Name="SSK Retail",Rating=5,DeliveryCharge=40 },
            new VendorDetail{Id=3,Name="ABB Retail",Rating=3,DeliveryCharge=60 },
            new VendorDetail{Id=4,Name="REL Retail",Rating=3,DeliveryCharge=70 },
            new VendorDetail{Id=5,Name="MFF Retail",Rating=5,DeliveryCharge=76 }

        };
    }

        [Test]
        public void Vendor_Test1()
        {
            Mock<IVendorDetailRepo> mock = new Mock<IVendorDetailRepo>();
            mock.Setup(p => p.GetVenderbyId(1)).Returns((ls.Where(x => x.Id == 1)).FirstOrDefault());
            VendorController obj = new VendorController(mock.Object);

            var res = obj.Get(1) as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }
        [Test]
        public void Vendor_Test2()
        {
            Mock<IVendorDetailRepo> mock = new Mock<IVendorDetailRepo>();
            mock.Setup(p => p.GetVenderbyId(55)).Returns((ls.Where(x => x.Id == 55)).FirstOrDefault());
            VendorController obj = new VendorController(mock.Object);

            var res = obj.Get(1) as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }
        


    }
}