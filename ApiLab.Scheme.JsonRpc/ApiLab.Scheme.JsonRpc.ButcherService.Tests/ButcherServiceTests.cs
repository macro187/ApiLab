using System;
using V20 = ApiLab.Meats.V20;
using V11 = ApiLab.Meats.V11;
using V10 = ApiLab.Meats.V10;
using ApiLab.Normal.Butcher.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiLab.Scheme.JsonRpc.Infrastructure.DotNetJsonRpcClient;

namespace ApiLab.Scheme.JsonRpc.ButcherService.Tests
{
    [TestClass]
    public class ButcherServiceTests : NormalButcherTests
    {

        public V10.IButcher butcherV10;


        [TestInitialize]
        public override void TestInitialize()
        {
            butcherV20 = JsonRpcClient.Build<V20.IButcher>(new Uri("http://localhost:53990/v2"));
            butcherV11 = JsonRpcClient.Build<V11.IButcher>(new Uri("http://localhost:53990/v1"));
            butcherV10 = JsonRpcClient.Build<V10.IButcher>(new Uri("http://localhost:53990/v1"));
        }


        [TestMethod]
        public void Butcher_V10_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV10.GetChicken().Name);
        }

    }
}
