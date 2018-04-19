using System;
using ApiLab.Breads;
using ApiLab.Burritos;
using ApiLab.Meats;

namespace ApiLab.Scheme.InProc
{
    public class InProcBurritoShop : IBurritoShop
    {

        public InProcBurritoShop(IBakery bakery, IButcher butcher)
        {
            this.bakery = bakery;
            this.butcher = butcher;
        }


        readonly IBakery bakery;
        readonly IButcher butcher;


        public Burrito MakeBurrito(string name)
        {
            switch (name)
            {
                case "HealthyBurrito":
                    return new Burrito() {
                        Name = "HealthyBurrito",
                        Tortilla = bakery.GetTortilla("FlourTortilla"),
                        Meat = butcher.GetMeat("Chicken"),
                    };
                
                case "UnhealthyBurrito":
                    return new Burrito() {
                        Name = "UnhealthyBurrito",
                        Tortilla = bakery.GetTortilla("CornTortilla"),
                        Meat = butcher.GetMeat("PulledPork"),
                    };
                
                default:
                    throw new Exception("We don't offer {name}");
            }
        }

    }
}
