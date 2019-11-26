using System;
using System.Diagnostics;
using Burritos.V1;
using Breads.Bakery;
using Meats.Butcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burritos.BurritoShop.Tests
{
    [TestClass]
    public class BurritoShopTests
    {

        protected IBurritoShop burritoShop;


        [TestInitialize]
        public virtual void TestInitialize()
        {
            var bakery = new Bakery();
            var butcher = new Butcher();
            burritoShop = new BurritoShop(bakery, butcher);
        }


        [TestMethod]
        public void Burrito_Shop_Can_Make_Healthy_Burrito()
        {
            var burrito = burritoShop.MakeBurrito("HealthyBurrito");

            Assert.AreEqual("HealthyBurrito", burrito.Name);
            Assert.AreEqual("FlourTortilla", burrito.Tortilla.Name);
            Assert.AreEqual("Chicken", burrito.Meat.Name);

            Console.WriteLine($"Eating {burrito.Name}, yum!");
        }


        [TestMethod]
        public void Burrito_Shop_Can_Make_Unhealthy_Burrito()
        {
            var burrito = burritoShop.MakeBurrito("UnhealthyBurrito");

            Assert.AreEqual("UnhealthyBurrito", burrito.Name);
            Assert.AreEqual("CornTortilla", burrito.Tortilla.Name);
            Assert.AreEqual("PulledPork", burrito.Meat.Name);

            Console.WriteLine($"Eating {burrito.Name}, yum!");
        }


        [TestMethod]
        public void Burrito_Shop_Can_Make_Unhealthy_Burrito_Async()
        {
            var burritoTask = burritoShop.MakeBurritoAsync("UnhealthyBurrito");
            burritoTask.Wait();
            var burrito = burritoTask.Result;

            Assert.AreEqual("UnhealthyBurrito", burrito.Name);
            Assert.AreEqual("CornTortilla", burrito.Tortilla.Name);
            Assert.AreEqual("PulledPork", burrito.Meat.Name);

            Console.WriteLine($"Eating {burrito.Name}, yum!");
        }


        [TestMethod]
        public void Burrito_Shop_Can_Be_Lazy_Async()
        {
            var watch = new Stopwatch();
            watch.Start();
            burritoShop.BeLazyFor3SecondsAsync().Wait();
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds >= 3000);
        }

    }
}
