using System.Linq;
using ApiLab.Meats;

namespace ApiLab.OneTrue.Butcher
{
    public class OneTrueButcher : IButcher
    {

        static readonly Meat[] meats = {
            new Meat() { Name = "Chicken", Price = 1.00M },
            new Meat() { Name = "PulledPork", Price = 2.00M },
        };


        public Meat GetMeat(string name)
        {
            return meats.Single(t => t.Name == name);
        }

    }
}
