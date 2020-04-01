using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubSpotTest.Service.Interface
{
    public interface IHotSpotApiService
    {
        Task<dynamic> GetAllContacts();
    }
}
