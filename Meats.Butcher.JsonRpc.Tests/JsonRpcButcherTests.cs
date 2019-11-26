using System;
using MacroDotNetJsonRpcClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V10 = Meats.V10;
using V11 = Meats.V11;
using V2 = Meats.V2;

namespace JsonRpcButcherTests
{
    [TestClass]
    public class JsonRpcButcherTests
    {

        V10.IButcher butcherV10;
        V11.IButcher butcherV11;
        V2.IButcher butcherV2;


        [TestInitialize]
        public void TestInitialize()
        {
            butcherV10 = JsonRpcClient.Build<V10.IButcher>(new Uri("http://localhost:53990/v1"));
            butcherV11 = JsonRpcClient.Build<V11.IButcher>(new Uri("http://localhost:53990/v1"));
            butcherV2 = JsonRpcClient.Build<V2.IButcher>(new Uri("http://localhost:53990/v2"));
        }


        [TestMethod]
        public void Butcher_V10_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV10.GetChicken().Name);
        }


        [TestMethod]
        public void Butcher_V11_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV11.GetChicken().Name);
        }


        [TestMethod]
        public void Butcher_V11_Can_Get_Pulled_Pork()
        {
            Assert.AreEqual("PulledPork", butcherV11.GetPulledPork().Name);
        }


        [TestMethod]
        public void Butcher_V20_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV2.GetMeat("Chicken").Name);
        }


        [TestMethod]
        public void Butcher_V20_Can_Get_Pulled_Pork()
        {
            Assert.AreEqual("PulledPork", butcherV2.GetMeat("PulledPork").Name);
        }

    }
}
