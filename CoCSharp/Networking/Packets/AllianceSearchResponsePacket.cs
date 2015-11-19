namespace CoCSharp.Networking.Packets
{
    public class AllianceSearchResponsePacket : IPacket
    {
        public ushort ID { get { return 0x5EF6; } }

        public string SearchString;
        public ClanSearchInfo[] ClansSearchInfo;

        public void ReadPacket(PacketReader reader)
        {
            SearchString = reader.ReadString();
            ClansSearchInfo = new ClanSearchInfo[reader.ReadInt32()];
            for (int i = 0; i < ClansSearchInfo.Length; i++)
            {
                ClanSearchInfo info = new ClanSearchInfo();
                info.ReadPacket(reader);
                ClansSearchInfo[i] = info;
            }
        }

        public void WritePacket(PacketWriter writer)
        {
            writer.WriteString(SearchString);
            writer.WriteInt32(ClansSearchInfo.Length);
            for (int i = 0; i < ClansSearchInfo.Length; i++)
            {
            //    var info = ClansSearchInfo[i];
            //    writer.WriteInt64(info.ID);
            //    writer.WriteString(info.Name);
            //    writer.WriteInt32(info.Unknown1);
            //    writer.WriteInt32(info.Type);
            //    writer.WriteInt32(info.MemberCount);
            //    writer.WriteInt32(info.Trophies);
            //    writer.WriteInt32(info.RequiedTrophies);
            //    writer.WriteInt32(info.WarsWon);
            //    writer.WriteInt32(info.WarsLost);
            //    writer.WriteInt32(info.WarsDraw);
            //    writer.WriteInt32(info.Badge);
            //    writer.WriteInt32(info.Unknown3);
            //    writer.WriteInt32(info.Unknown4);
            //    writer.WriteInt32(info.EP);
            //    writer.WriteInt32(info.Level);
            }
        }

        public class ClanSearchInfo : BaseAllianceResponsePacket
        {
            public int Badge;
            public int EP;
            public int Level;
            internal int Unknown3;
            internal int Unknown4;

            public override void ReadPacket(PacketReader reader)
            {
                base.ReadPacket(reader);
                Badge = reader.ReadInt32();
                Unknown3 = reader.ReadInt32();
                Unknown4 = reader.ReadInt32();
                EP = reader.ReadInt32();
                Level = reader.ReadInt32();
            }
        }
    }
}
