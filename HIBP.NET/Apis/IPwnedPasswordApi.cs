using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        bool IsPasswordPwned(string plainTextPasswordOrPasswordHash);
        Task<bool> IsPasswordPwnedAsync(string plainTextPasswordOrPasswordHash);
        int IsPasswordPwnedSafe(string plainTextPassword);
        Task<int> IsPasswordPwnedSafeAsync(string plainTextPassword);
    }
}