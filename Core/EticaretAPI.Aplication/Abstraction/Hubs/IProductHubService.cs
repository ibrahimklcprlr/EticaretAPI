using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Abstraction.Hubs
{
    public interface IProductHubService
    {
        Task ProductaddedMessage(string message);
    }
}
