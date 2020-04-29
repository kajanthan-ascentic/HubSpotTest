using Hubspot.Sync.Account.Common;
using Hubspot.Sync.Account.Common.Models.Hubspot;
using Hubspot.Sync.Account.Common.Services.Interface;
using Hubspot.Sync.Account.Common.Services.Service.Synchronize;
using System;
using System.Collections.Generic;
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
            bool syncEnabled = true;
            Console.WriteLine("Property Group begin");

            List<string> objectType = new List<string>() { "contact", "company", "deal" };

            Console.WriteLine("Property Group begin");

            foreach (var item in objectType)
            {
                ISync<PropertyGroup> pgSync = new PropertyGroupSync(sourceKey, 
                                                                    destinationKey, 
                                                                    item, 
                                                                    syncEnabled);

                if (pgSync != null) 
                {
                    await pgSync.Sync();
                }
            }

            Console.WriteLine("Property Group end");

            Console.WriteLine("Property begin");

            foreach (var item in objectType)
            {
                ISync<PropertyModel> pSync = new PropertySync(sourceKey,
                                                                    destinationKey,
                                                                    item,
                                                                    syncEnabled);

                if (pSync != null)
                {
                    await pSync.Sync();
                }
            }

            Console.WriteLine("Property end");

            Console.WriteLine("Sync Process End");

            Console.ReadLine();

        }
    }
}
