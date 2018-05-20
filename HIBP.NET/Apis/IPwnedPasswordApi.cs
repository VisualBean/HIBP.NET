using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        int IsPasswordPwned(string plainTextPassword);
        Task<int> IsPasswordPwnedAsync(string plainTextPassword);
    }
}