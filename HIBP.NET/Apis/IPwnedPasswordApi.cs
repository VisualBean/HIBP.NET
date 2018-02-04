using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        bool IsPasswordPwned(string plainTextPasswordOrPasswordHash);
        Task<bool> IsPasswordPwnedAsync(string plainTextPasswordOrPasswordHash);
    }
}