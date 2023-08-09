using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.TimeReal.Services
{
    public interface IHubConnectionService
    {
        public HubConnection GetHubConnection();
    }
}
