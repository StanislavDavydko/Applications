using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace B_Application.Web
{
    public class PriceHubClient
    {
        private HubConnection hubConnection;

        public PriceHubClient(string hubUrl)
        {
            hubConnection = new HubConnectionBuilder()
                .Build();
        }

        public async Task StartAsync()
        {
            await hubConnection.StartAsync();
        }

        public void RegisterPriceUpdate(Action<string> callback)
        {
            hubConnection.On("Receive", callback);
        }
    }

}
