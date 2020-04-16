using System;
using System.Threading.Tasks;

namespace HubSpotTest.Service.Interface
{
    public interface ITokenService
    {
        Task<string> GetToken(string code);
    }
}
