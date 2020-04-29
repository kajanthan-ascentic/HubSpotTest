using Hubspot.Sync.Account.Common;
using Hubspot.Sync.Account.Common.Models.Hubspot;
using Hubspot.Sync.Account.Common.Services.Interface;
using Hubspot.Sync.Account.Common.Services.Service.Synchronize;
using System;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account
{
    class Program
    {
        async static Task Main(string[] args)
        {
            Console.WriteLine("Sync Process begin");

            string sourceKey = Constants.KEY_API_DEV;
            string destinationKey = Constants.KEY_API_QA;

            ISync<PropertyGroup> pgContactSync = new PropertyGroupSync(sourceKey, destinationKey, "contact");

            await pgContactSync.Sync();

            ISync<PropertyGroup> pgCompanySync = new PropertyGroupSync(sourceKey, destinationKey, "company");

            await pgCompanySync.Sync();

            ISync<PropertyGroup> pgDealsSync = new PropertyGroupSync(sourceKey, destinationKey, "deal");

            await pgDealsSync.Sync();

            Console.WriteLine("Sync Process End");

        }
    }
}
