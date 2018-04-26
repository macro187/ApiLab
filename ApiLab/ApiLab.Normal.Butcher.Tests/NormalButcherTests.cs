using ApiLab.Meats.Compat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V11 = ApiLab.Meats.V11;
using V20 = ApiLab.Meats.V20;

namespace ApiLab.Normal.Butcher.Tests
{
    [TestClass]
    public class NormalButcherTests
    {

        public V11.IButcher butcherV11;
        public V20.IButcher butcherV20;


        [TestInitialize]
        public virtual void TestInitialize()
        {
            butcherV20 = new NormalButcher();
            butcherV11 = new ButcherV2ToV1Adapter(butcherV20);
        }


        [TestMethod]
        public void Butcher_V20_Can_Get_Chicken()
        {
            Assert.AreEqual("Chicken", butcherV20.GetMeat("Chicken").Name);
        }


        [TestMethod]
        public void Butcher_V20_Can_Get_Pulled_Pork()
        {
            Assert.AreEqual("PulledPork", butcherV20.GetMeat("PulledPork").Name);
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

    }
}
