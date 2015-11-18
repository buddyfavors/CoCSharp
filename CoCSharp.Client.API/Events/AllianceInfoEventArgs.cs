using CoCSharp.Networking.Packets;
using System;

namespace CoCSharp.Client.API.Events
{
    public class AllianceInfoEventArgs : EventArgs
    {
        public AllianceInfoEventArgs(AllianceInfoResponsePacket packet)
        {
            Packet = packet;
        }

        public AllianceInfoResponsePacket Packet { get; private set; }
    }
}
