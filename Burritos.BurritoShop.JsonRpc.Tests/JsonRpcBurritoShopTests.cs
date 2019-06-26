using System;
using System.Threading.Tasks;
using Burritos.V1;
using Burritos.BurritoShop.Tests;
using MacroDotNetJsonRpcClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Burritos.BurritoShop.JsonRpc.Tests
{
    [TestClass]
    public class JsonRpcBurritoShopTests : BurritoShopTests
    {

        IBatched<IBurritoShop> burritoShopBatched;


        [TestInitialize]
        public override void TestInitialize()
        {
            burritoShop = JsonRpcClient.Build<IBurritoShop>(new Uri("http://localhost:57982/v1"));
            burritoShopBatched = (IBatched<IBurritoShop>)burritoShop;
        }


        [TestMethod]
        public async Task Burrito_Shop_Can_Make_Burritos_In_Batches()
        {
            var (healthyBurrito, unhealthyBurrito) =
                await burritoShopBatched.InvokeBatch(
                    burritoShop => burritoShop.MakeBurrito("HealthyBurrito"),
                    burritoShop => burritoShop.MakeBurrito("UnhealthyBurrito"));

            Assert.AreEqual("HealthyBurrito", healthyBurrito.Name);
            Assert.AreEqual("FlourTortilla", healthyBurrito.Tortilla.Name);
            Assert.AreEqual("Chicken", healthyBurrito.Meat.Name);

            Assert.AreEqual("UnhealthyBurrito", unhealthyBurrito.Name);
            Assert.AreEqual("CornTortilla", unhealthyBurrito.Tortilla.Name);
            Assert.AreEqual("PulledPork", unhealthyBurrito.Meat.Name);

            Console.WriteLine($"Eating {healthyBurrito.Name}, yum!");
            Console.WriteLine($"Eating {unhealthyBurrito.Name}, yum!");
        }

    }
}
