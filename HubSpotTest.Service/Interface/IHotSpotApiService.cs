using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubSpotTest.Model;

namespace HubSpotTest.Service.Interface
{
    public interface IHotSpotApiService
    {
        Task<dynamic> GetAllContacts();
        Task<dynamic> AddContact(Contact contact);
    }
}
