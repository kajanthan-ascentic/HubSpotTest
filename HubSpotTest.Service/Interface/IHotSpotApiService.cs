using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubSpotTest.Model;

namespace HubSpotTest.Service.Interface
{
    public interface IHotSpotApiService
    { 
        Task<dynamic> GetAllContacts();

        Task<dynamic> GetContactById(string id);

        Task<string> CreateContact(ContactModel contact);

        Task<string> UpdateContact(string id, ContactModel contact);

        Task<string> DeleteContact(string id);


        Task<dynamic> GetAllCompanies();

        Task<dynamic> GetCompanyById(string id);

        Task<string> CreateCompany(CompanyModel company);

        Task<string> UpdateCompany(string id, CompanyModel company);

        Task<string> DeleteCompany(string id);


    }
}
