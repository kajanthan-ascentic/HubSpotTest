using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account.Common.Services.Interface
{
    public interface IHttpClientService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string path);
        Task<T> GetAsync(string path);
        Task<string> GetAsyncAsString(string path);
        Task<T> PostAsync(string path, object model);
        Task<string> PostAsyncAsString(string path, object model);
    }
}
