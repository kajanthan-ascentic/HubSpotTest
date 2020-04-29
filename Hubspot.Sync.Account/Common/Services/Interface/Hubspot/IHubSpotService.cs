using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account.Common.Services.Interface.Hubspot
{

    public interface IHubSpotService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByName();
        Task<T> Post(object model);
    }
}
