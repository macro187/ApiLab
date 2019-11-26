using System.Linq;
using Breads.V1;

namespace Breads.Bakery
{
    public class Bakery : IBakery
    {

        static readonly Tortilla[] tortillas = {
            new Tortilla() { Name = "CornTortilla", Price = 1.00M },
            new Tortilla() { Name = "FlourTortilla", Price = 2.00M },
        };


        public Tortilla GetTortilla(string name)
        {
            return tortillas.Single(t => t.Name == name);
        }

    }
}
