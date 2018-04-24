using System;
using ApiLab.Meats.Compat;
using ApiLab.Normal.Butcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V11 = ApiLab.Meats.V11;
using V20 = ApiLab.Meats.V20;

namespace ApiLab.Tests
{
    [TestClass]
    public class MeatsCompatTests
    {

        V11.IButcher butcherV1;


        [TestInitialize]
        public void TestInitialize()
        {
            V20.IButcher butcherV2 = new NormalButcher();
            butcherV1 = new ButcherV2ToV1Adapter(butcherV2);
        }


        [TestMethod]
        public void Butcher_V1_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV1.GetChicken().Name);
        }


        [TestMethod]
        public void Butcher_V1_Can_Get_Pulled_Pork()
        {
            Assert.AreEqual("PulledPork", butcherV1.GetPulledPork().Name);
        }

    }
}
