using System;
using System.Threading.Tasks;
using ApiLab.Burritos;
using ApiLab.Normal.BurritoShop.Tests;
using ApiLab.Scheme.JsonRpc.Infrastructure.DotNetJsonRpcClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiLab.Scheme.JsonRpc.BurritoShopService.Tests
{
    [TestClass]
    public class BurritoShopServiceTests : NormalBurritoShopTests
    {

        IBatched<IBurritoShop> burritoShopBatched;


        [TestInitialize]
        public override void TestInitialize()
        {
            burritoShop = JsonRpcClient.Build<IBurritoShop>(new Uri("http://localhost:57982/"));
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
