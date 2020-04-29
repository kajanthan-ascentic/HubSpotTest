using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account.Common.Services.Interface
{
    public interface ISync <T>
    {
        Task Sync();
    }
}
