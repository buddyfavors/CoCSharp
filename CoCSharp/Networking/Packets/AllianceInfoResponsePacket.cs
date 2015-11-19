﻿using System.Collections.Generic;

namespace CoCSharp.Networking.Packets
{
    public class AllianceInfoResponsePacket : IPacket
    {
        public ushort ID { get { return 0x5EED; } }

        internal long ClanID;
        internal string ClanName;
        internal int MembersCount;
        internal int TotalPoints;
        internal int RequiedTrophies;
        internal int WarsWon;
        internal int Level;
        internal WarFrequencies WarFrequency;
        internal ClanTypes ClanType;
        internal ushort ClanLocation;
        internal int ClanPerksPoints;
        internal string Description;
        internal List<AllianceMemberInfo> Members;
        internal byte LegacyMembersCount;
        internal int Unknown3;
        internal int Unknown4;
        internal int Unknown5;
        internal int Unknown8;
        internal int Unknown9;
        internal int Unknown10;
        internal int Unknown11;
        internal byte Unknown6543;
        internal byte Unknown3214;
        internal byte Unknown7861;
        internal byte Unknown5167;
        internal byte Unknown6451;
        internal byte Unknown2314;
        internal byte Unknown7865;
        internal byte Unknown4567;
        internal byte Unknown3247;
        internal byte Unknown4657;
        internal byte Unknown8974;
        internal byte Unknown3217;

        public void ReadPacket(PacketReader reader)
        {
            ClanID = reader.ReadInt64();
            ClanName = reader.ReadString();
            Unknown6451 = reader.ReadByte();
            Unknown2314 = reader.ReadByte();
            Unknown7865 = reader.ReadByte();
            Unknown4567 = reader.ReadByte();
            Unknown3247 = reader.ReadByte();
            Unknown4657 = reader.ReadByte();
            Unknown8974 = reader.ReadByte();
            ClanType = (ClanTypes)reader.ReadByte();
            MembersCount = reader.ReadInt32();
            TotalPoints = reader.ReadInt32();
            RequiedTrophies = reader.ReadInt32();
            WarsWon = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadInt32();
            Unknown6543 = reader.ReadByte();
            Unknown3214 = reader.ReadByte();
            Unknown7861 = reader.ReadByte();
            WarFrequency = (WarFrequencies)reader.ReadByte();
            Unknown5167 = reader.ReadByte();
            Unknown3217 = reader.ReadByte();
            ClanLocation = reader.ReadUInt16();
            ClanPerksPoints = reader.ReadInt32();
            Level = reader.ReadInt32();
            Description = reader.ReadString();
            Unknown8 = reader.ReadInt32();
            Unknown9 = reader.ReadInt32();

            LegacyMembersCount = reader.ReadByte();

            // It appears this is some legacy data, only some clans don't have this data.
            // It seemed to trend towards old clans, newer clans didn't have an issue.
            if (LegacyMembersCount != MembersCount)
            {
                Unknown10 = reader.ReadInt32();
                Unknown11 = reader.ReadInt32();
            }

            Members = new List<AllianceMemberInfo>();

            for (int i = 0; i < MembersCount; i++)
            {
                var allianceMember = new AllianceMemberInfo();
                allianceMember.UserID = reader.ReadInt64();
                allianceMember.Name = reader.ReadString();
                allianceMember.Role = reader.ReadInt32();
                allianceMember.Level = reader.ReadInt32();
                allianceMember.League = reader.ReadInt32();
                allianceMember.Trophies = reader.ReadInt32();
                allianceMember.TroopsDonated = reader.ReadInt32();
                allianceMember.TroopsReceived = reader.ReadInt32();
                allianceMember.Rank = reader.ReadInt32();
                allianceMember.PreviousRank = reader.ReadInt32();
                allianceMember.NewMember = reader.ReadByte();
                allianceMember.Unknown1 = reader.ReadByte();
                allianceMember.Unknown2 = reader.ReadInt32();
                allianceMember.Unknown3 = reader.ReadInt32();
                allianceMember.Unknown4 = reader.ReadInt32();
                allianceMember.Unknown5 = reader.ReadInt32();
                Members.Add(allianceMember);
            }
        }

        public void WritePacket(PacketWriter writer)
        {
            writer.WriteInt64(ClanID);
            writer.WriteString(ClanName);
            //writer.WriteInt32(Unknown1);
            //writer.WriteInt32(Unknown2);
            writer.WriteInt32(MembersCount);
            writer.WriteInt32(TotalPoints);
            writer.WriteInt32(RequiedTrophies);
            writer.WriteInt32(WarsWon);
            //writer.WriteInt32(Unknown3);
            writer.WriteInt32(Level);
            //writer.WriteInt32(Sheild);
            //writer.WriteInt32(WarFrequency);
            //writer.WriteInt32(Unknown4);
            writer.WriteInt32(ClanPerksPoints);
            //writer.WriteInt32(Unknown5);
            writer.WriteString(Description);
            //writer.WriteInt32(Unknown6);
            //writer.WriteInt32(Unknown7);

            writer.WriteInt32(Members.Count);
            for (int i = 0; i < Members.Count; i++)
            {
                var allianceMember = Members[i];
                writer.WriteInt64(allianceMember.UserID);
                writer.WriteString(allianceMember.Name);
                writer.WriteInt32(allianceMember.Role);
                writer.WriteInt32(allianceMember.Level);
                writer.WriteInt32(allianceMember.League);
                writer.WriteInt32(allianceMember.Trophies);
                writer.WriteInt32(allianceMember.TroopsDonated);
                writer.WriteInt32(allianceMember.TroopsReceived);
                writer.WriteInt32(allianceMember.Rank);
                writer.WriteInt32(allianceMember.PreviousRank);
                writer.WriteInt32(allianceMember.NewMember);
                writer.WriteInt32(allianceMember.ClanWarPreference);
                writer.WriteInt32(allianceMember.ClanWarPreference1);
                writer.WriteInt32(allianceMember.Unknown1);
                writer.WriteInt64(allianceMember.UserID1);
            }
        }

        //TODO: Make kooler
        public class AllianceMemberInfo
        {
            public long UserID;
            public string Name;
            public int Role;
            public int Level;
            public int League;
            public int Trophies;
            public int TroopsDonated;
            public int TroopsReceived;
            public int Rank;
            public int PreviousRank;
            public byte NewMember;
            public int ClanWarPreference;
            public int ClanWarPreference1;
            internal int Unknown1;
            public long UserID1;
            internal int Unknown2;
            internal int Unknown3;
            internal int Unknown4;
            internal int Unknown5;
        }

        public enum WarFrequencies : byte
        {
            Always = 1,
            Twice = 2,
            Once = 3,
            Rarely = 4,
            Never = 5
        }

        public enum ClanTypes : byte
        {
            Anyone = 1,
            Closed = 2,
            InviteOnly = 3
        }
    }
}
