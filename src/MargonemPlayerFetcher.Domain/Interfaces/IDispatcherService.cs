using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Interfaces
{
    public interface IDispatcherService
    {
        Task DispatchEquipmentSync();
        Task DispatchPlayerSync();

    }
}
