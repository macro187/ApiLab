using System;
using System.Diagnostics;
using ApiLab.Burritos;
using ApiLab.Normal.Bakery;
using ApiLab.Normal.BurritoShop;
using ApiLab.Normal.Butcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiLab.Normal.BurritoShop.Tests
{
    [TestClass]
    public class NormalBurritoShopTests
    {

        IBurritoShop burritoShop;


        public virtual IBurritoShop GetBurritoShop()
        {
            var bakery = new NormalBakery();
            var butcher = new NormalButcher();
            return new NormalBurritoShop(bakery, butcher);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            burritoShop = GetBurritoShop();
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
            Assert.IsTrue(watch.ElapsedMilliseconds > 3000);
        }

    }
}
