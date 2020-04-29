using Hubspot.Sync.Account.Common.Services.Interface;
using Hubspot.Sync.Account.Common.Services.Interface.Hubspot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Hubspot.Sync.Account.Common.Services.Service.Synchronize
{
    public abstract class BaseSynchronize<T> : ISync<T>
    {
        protected readonly IHttpClientService<T> _sourceClient = null;
        protected readonly IHttpClientService<T> _destinationClient = null;

        protected IHubSpotService<T> _sourceService = null;
        protected IHubSpotService<T> _destinationService = null;

        protected IEnumerable<T> _sourceList = null;
        protected IEnumerable<T> _destinationList = null;
        protected IEnumerable<T> _missingList = null;

        protected string _objectType = String.Empty;

        public BaseSynchronize(string sourceAPIKey, string destinationAPIKey, string objectType) 
        {
            this._sourceClient = new HttpClientService<T>(sourceAPIKey);
            this._destinationClient = new HttpClientService<T>(destinationAPIKey);
            this._objectType = objectType;
        }

        private async Task PopulateLists() 
        {
            this._sourceList = await this._sourceService.GetAll();
            
            Console.WriteLine("Source count : " + this._sourceList.Count().ToString());

            this._destinationList = await this._destinationService.GetAll();
            Console.WriteLine("Destination count : " + this._destinationList.Count().ToString());

            this._missingList = this.GetMissingList();

            this._missingList = this.GetOrderList(this._missingList);
            Console.WriteLine("Missing count : " + this._missingList.Count().ToString());
        }

        protected abstract IEnumerable<T> GetMissingList();

        protected abstract IEnumerable<T> GetOrderList(IEnumerable<T> list);

        protected abstract string GetConsoleText(T model);

        protected abstract object GetInsertModel(T model);

        public async Task Sync()
        {
            Console.WriteLine("Begin : " + this.GetType().Name + (String.IsNullOrEmpty(this._objectType) ?  " object type : " + this._objectType : String.Empty));

            await this.PopulateLists();

            foreach (T item in this._missingList)
            {
                Console.WriteLine(this.GetConsoleText(item));

                await this._destinationService.Post(this.GetInsertModel(item));
            }

            Console.WriteLine("End : " + this.GetType().Name + (String.IsNullOrEmpty(this._objectType) ? " object type : " + this._objectType : String.Empty));
        }
    }
}
