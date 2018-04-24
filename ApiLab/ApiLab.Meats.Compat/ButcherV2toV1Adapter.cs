namespace ApiLab.Meats.Compat
{
    public class ButcherV2ToV1Adapter
        : V11.IButcher
    {

        static V11.Meat ToV1(V20.Meat meatV2)
        {
            return new V11.Meat() {
                Name = meatV2.Name,
                Price = meatV2.Price
            };
        }
        

        public ButcherV2ToV1Adapter(V20.IButcher butcherV2)
        {
            this.butcherV2 = butcherV2;
        }


        readonly V20.IButcher butcherV2;


        public V11.Meat GetChicken()
        {
            return ToV1(butcherV2.GetMeat("Chicken"));
        }


        public V11.Meat GetPulledPork()
        {
            return ToV1(butcherV2.GetMeat("PulledPork"));
        }

    }
}
