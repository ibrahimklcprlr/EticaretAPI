using EticaretAPI.Aplication.Abstraction.Hubs;
using EticaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.SignalR.HubServices
{
    public class ProducthubService : IProductHubService
    {
        IHubContext<ProductHub> _hubContext;

        public ProducthubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductaddedMessage(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
