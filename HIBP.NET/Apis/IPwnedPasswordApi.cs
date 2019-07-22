using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        Task<int> IsPasswordPwnedAsync(string plainTextPassword, bool isHash = false);
    }
}