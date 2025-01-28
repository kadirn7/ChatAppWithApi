using System.Data;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Helpers
{
    public class SignalrConnection : ISignalrConnection
    {
        HubConnection _connection;
        public bool IsConnected()
        {
            return _connection != null&& _connection.State ==HubConnectionState.Connected; 
        }

        public HubConnection StartConnection()
        {
            var hostInfo = "https=//localhost:7184";
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                return _connection;
            }
            _connection = new HubConnectionBuilder()
                .WithUrl($"{hostInfo}/chathub")
                .WithKeepAliveInterval(TimeSpan.FromDays(1))
                .WithServerTimeout(TimeSpan.FromDays(1))
                .WithAutomaticReconnect()
                .Build();

            if (_connection.State == HubConnectionState.Disconnected)
            {
                _connection.StartAsync();
            }

            return _connection;
        }
    }
}
