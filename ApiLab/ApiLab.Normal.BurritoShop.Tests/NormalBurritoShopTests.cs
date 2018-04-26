using System;
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

    }
}
