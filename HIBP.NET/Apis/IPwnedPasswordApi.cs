using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        int IsPasswordPwned(string plainTextPasswordOrPasswordHash);
        Task<int> IsPasswordPwnedAsync(string plainTextPasswordOrPasswordHash);
        int IsPasswordPwnedSafe(string plainTextPassword);
        Task<int> IsPasswordPwnedSafeAsync(string plainTextPassword);
    }
}