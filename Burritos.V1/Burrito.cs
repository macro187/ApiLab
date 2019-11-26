using Breads.V1;
using Meats.V2;

namespace Burritos.V1
{
    public class Burrito
    {

        public string Name { get; set; }

        public Tortilla Tortilla { get; set; }

        public Meat Meat { get; set; }

    }
}
