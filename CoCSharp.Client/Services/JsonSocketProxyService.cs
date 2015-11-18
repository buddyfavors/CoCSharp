using CoCSharp.Client.API.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CoCSharp.Client.Services
{
    // Communication is done with JSON and the protocol at http://www.sebastianseilund.com/json-socket-sending-json-over-tcp-in-node.js-using-sockets
    // TODO: can this be a plugin?
    public class JsonSocketProxyService : IProxyService
    {
        private static TcpListener ProxyConnection { get; set; }
        private static ConcurrentDictionary<IntPtr, Socket> ProxyClients { get; set; }

        void IProxyService.Start()
        {
            ProxyClients = new ConcurrentDictionary<IntPtr, Socket>();
            new Thread(async t =>
            {
                ProxyConnection = new TcpListener(IPAddress.Any, 3939);
                ProxyConnection.Start();

                while (true)
                {
                    var socket = await ProxyConnection.AcceptSocketAsync();
                    ProxyClients.Clear();
                    ProxyClients.TryAdd(socket.Handle, socket);

                    var socketAsyncEventArgs = new SocketAsyncEventArgs();
                    byte[] buffer = new byte[8192];
                    socketAsyncEventArgs.SetBuffer(buffer, 0, buffer.Length);
                    socketAsyncEventArgs.Completed += SocketAsyncEventArgs_Completed;
                    socket.ReceiveAsync(socketAsyncEventArgs);
                }
            }).Start();
        }

        private static void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
            var bufferIndex = data.IndexOf('#');
            var jObject = bufferIndex != -1 ? (JObject)JsonConvert.DeserializeObject(data.Substring(bufferIndex + 1)) : null;

            if (jObject.Value<int>("PacketId") != 14302)
                return;

            Program.Client.SendAllianceInfoRequest(jObject.Value<long>("ClanId"));
        }

        public void OnAllianceInfo(object sender, AllianceInfoEventArgs e)
        {
            var json = JsonConvert.SerializeObject(e.Packet);
            var buffer = Encoding.UTF8.GetBytes(string.Format("{0}#{1}", json.Length, json));

            foreach (var proxyClient in ProxyClients)
            {
                proxyClient.Value.Send(buffer, SocketFlags.None);
            }
        }
    }
}
