using System;
using System.Net.Sockets;
using System.Text;

namespace UDPForwarder.Services
{
    public class UDPService : ITransportService
    {
        private string serverIp;
        private int serverPort;

        public UDPService(string _serverIp , int _serverPort)
        {
            serverIp = _serverIp;
            serverPort = _serverPort;
        }

        public void SendLog(string info)
        {
            var udpClient = new UdpClient(serverIp, serverPort);
            var sendBytes = Encoding.UTF8.GetBytes(info);

            udpClient.Send(sendBytes, sendBytes.Length);         
        }
    }   
}
