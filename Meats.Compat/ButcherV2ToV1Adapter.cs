namespace Meats.Compat
{
    public class ButcherV2ToV1Adapter : V11.IButcher
    {

        readonly V2.IButcher butcher;


        public ButcherV2ToV1Adapter(V2.IButcher butcher)
        {
            this.butcher = butcher;
        }


        public V11.Meat GetChicken()
        {
            return ToV1(butcher.GetMeat("Chicken"));
        }


        public V11.Meat GetPulledPork()
        {
            return ToV1(butcher.GetMeat("PulledPork"));
        }


        static V11.Meat ToV1(V2.Meat meat)
        {
            return new V11.Meat() {
                Name = meat.Name,
                Price = meat.Price
            };
        }

    }
}
