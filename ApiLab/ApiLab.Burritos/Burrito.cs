using ApiLab.Breads;
using ApiLab.Meats.V20;

namespace ApiLab.Burritos
{
    public class Burrito
    {
        
        public string Name { get; set; }

        public Tortilla Tortilla { get; set; }

        public Meat Meat { get; set; }

    }
}