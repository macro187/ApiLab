using System.Threading.Tasks;

namespace Burritos.V1
{
    public interface IBurritoShop
    {

        Task BeLazyFor3SecondsAsync();

        Task<Burrito> MakeBurritoAsync(string name);

        Burrito MakeBurrito(string name);

    }
}
