using System;

namespace CoCSharp.Networking.Packets
{
    public abstract class BaseAllianceResponsePacket : IPacket
    {
        public virtual ushort ID { get { return 0; } }

        public long ClanID;
        public string ClanName;
        public int MembersCount;
        public int Trophies;
        public int RequiedTrophies;
        public int WarsWon;
        public int WarsLost;
        public int WarsDraw;
        public ClanTypes ClanType;
        internal int Unknown6451;
        internal short Unknown3247;
        internal byte Unknown8974;

        public virtual void ReadPacket(PacketReader reader)
        {
            ClanID = reader.ReadInt64();
            ClanName = reader.ReadString();
            Unknown6451 = reader.ReadInt32();
            Unknown3247 = reader.ReadInt16();
            Unknown8974 = reader.ReadByte();
            ClanType = (ClanTypes)reader.ReadByte();
            MembersCount = reader.ReadInt32();
            Trophies = reader.ReadInt32();
            RequiedTrophies = reader.ReadInt32();
            WarsWon = reader.ReadInt32();
            WarsLost = reader.ReadInt32();
            WarsDraw = reader.ReadInt32();
        }

        public virtual void WritePacket(PacketWriter writer)
        {
        }
    }
}
