using ApiLab.OneTrue.Bakery;
using ApiLab.OneTrue.Butcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiLab.Scheme.InProc.Tests
{
    [TestClass]
    public class InProcBurritoShopTests
    {

        public TestContext TestContext { get; set; }


        InProcBurritoShop burritoShop;


        [TestInitialize]
        public void TestInitialize()
        {
            burritoShop = new InProcBurritoShop(new OneTrueBakery(), new OneTrueButcher());
        }


        [TestMethod]
        public void Eat_Healthy_Burrito()
        {
            var burrito = burritoShop.MakeBurrito("HealthyBurrito");

            Assert.AreEqual("HealthyBurrito", burrito.Name);
            Assert.AreEqual("FlourTortilla", burrito.Tortilla.Name);
            Assert.AreEqual("Chicken", burrito.Meat.Name);

            TestContext.WriteLine($"Eating {burrito.Name}, yum!");
        }


        [TestMethod]
        public void Eat_Unhealthy_Burrito()
        {
            var burrito = burritoShop.MakeBurrito("UnhealthyBurrito");

            Assert.AreEqual("UnhealthyBurrito", burrito.Name);
            Assert.AreEqual("CornTortilla", burrito.Tortilla.Name);
            Assert.AreEqual("PulledPork", burrito.Meat.Name);

            TestContext.WriteLine($"Eating {burrito.Name}, yum!");
        }

    }
}
