using System.Threading.Tasks;

namespace HIBP
{
    public interface IPwnedPasswordApi
    {
        Task<bool> IsPasswordPwned(string plainTextPasswordOrPasswordHash);
    }
}