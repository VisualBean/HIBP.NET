namespace HIBP.Apis
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HIBP.Responses;

    public class PastesApi : BaseApi, IPastesApi
    {
        public PastesApi(ApiKey apiKey, string serviceName)
            : base(apiKey, serviceName)
        {
        }

        public async Task<IEnumerable<Paste>> GetPastesAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<Paste>>($"pasteaccount/{email}", cancellationToken);
        }
    }
}
