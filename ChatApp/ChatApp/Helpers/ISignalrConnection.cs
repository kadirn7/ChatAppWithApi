using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Helpers
{
    public interface ISignalrConnection
    {
        HubConnection StartConnection();
        bool IsConnected();
    }
}
