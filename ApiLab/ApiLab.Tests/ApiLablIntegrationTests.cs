using System;
using ApiLab.Normal.Bakery;
using ApiLab.Normal.BurritoShop;
using ApiLab.Normal.Butcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiLab.Tests
{
    [TestClass]
    public class IntegrationTests
    {

        NormalBurritoShop burritoShop;


        [TestInitialize]
        public void TestInitialize()
        {
            var bakery = new NormalBakery();
            var butcher = new NormalButcher();
            burritoShop = new NormalBurritoShop(bakery, butcher);
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
