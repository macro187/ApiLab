using System;
using System.Threading.Tasks;
using Breads.V1;
using Burritos.V1;
using Meats.V2;

namespace Burritos.BurritoShop
{
    public class BurritoShop : IBurritoShop
    {

        public BurritoShop(IBakery bakery, IButcher butcher)
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
                    throw new Exception($"We don't offer {name}");
            }
        }


        public Task<Burrito> MakeBurritoAsync(string name)
        {
            return Task.FromResult(MakeBurrito(name));
        }


        public Task BeLazyFor3SecondsAsync()
        {
            return Task.Delay(3000);
        }

    }
}
