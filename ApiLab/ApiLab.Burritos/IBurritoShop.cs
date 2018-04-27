using System.Threading.Tasks;

namespace ApiLab.Burritos
{
    public interface IBurritoShop
    {

        Task BeLazyFor3SecondsAsync();

        Task<Burrito> MakeBurritoAsync(string name);

        Burrito MakeBurrito(string name);

    }
}
