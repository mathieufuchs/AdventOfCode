namespace AdventOfCode
{
    public class Solver16 : ISolver
    {
        

        public Solver16()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var decoded = input.Decode();

            var (reminder, packet) = decoded.CreatePacket();

            return packet.GetVersion().ToString();
        }

        public async Task<string> Solve2(string input)
        {
            var decoded = input.Decode();

            var (reminder, packet) = decoded.CreatePacket();

            return packet.GetValue().ToString();
        }
    }

    public interface IPacket
    {
        int Version { get; }
        PacketType PacketType { get; }
        int GetVersion();
        long GetValue();
    }

    public abstract class Packet
    {
        public Packet(int version, PacketType packetType)
        {
            Version = version;
            PacketType = packetType;
        }
        public int Version { get;}
        public PacketType PacketType { get; }

        public abstract int GetVersion();
        public abstract long GetValue();
    }

    public class OperatorPacket : Packet, IPacket
    {
        public IPacket[] SubPackets { get; }

        public OperatorPacket(int version, PacketType packetType, IPacket[] subPackets): base(version, packetType)
        {
            SubPackets = subPackets;   
        }

        public override int GetVersion()
        {
            return Version + SubPackets.Sum(s => s.GetVersion());
        }

        public override long GetValue()
        {
            return PacketType switch
            {
                PacketType.Sum => SubPackets.Sum(sp => sp.GetValue()),
                PacketType.Product => SubPackets.Aggregate(1L, (acc, sp) => acc * sp.GetValue()),
                PacketType.Minimum => SubPackets.Min(sp => sp.GetValue()),
                PacketType.Maximum => SubPackets.Max(sp => sp.GetValue()),
                PacketType.GreaterThan => SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1 : 0,
                PacketType.LessThan => SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0,
                PacketType.EqualTo => SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1 : 0,
                _ => throw new NotImplementedException()
            };
        }
    }

    public class LiteralPacket : Packet, IPacket
    {
        public string LiteralValue { get; }    
        public LiteralPacket(int version, PacketType packetType, string literalValue) : base(version, packetType)
        {
            LiteralValue = literalValue;
        }

        public override long GetValue() => Convert.ToInt64(LiteralValue, 2);

        public override int GetVersion()
        {
            return Version;
        }
    }

    public static class PacketExtensions
    {
        private static readonly Dictionary<char, string> decodeTable = new()
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

        private static readonly Dictionary<string, int> versionDecodeTable = new()
        {
            { "000", 0 },
            { "001", 1 },
            { "010", 2 },
            { "011", 3 },
            { "100", 4 },
            { "101", 5 },
            { "110", 6 },
            { "111", 7 }
        };

        private static readonly Dictionary<string, PacketType> packetTypeDecodeTable = new()
        {
            { "000", PacketType.Sum },
            { "001", PacketType.Product },
            { "010", PacketType.Minimum },
            { "011", PacketType.Maximum},
            { "100", PacketType.Literal },
            { "101", PacketType.GreaterThan },
            { "110", PacketType.LessThan },
            { "111", PacketType.EqualTo }
        };


        public static string Decode(this string input)
        {
            return string.Concat(input.Select(c => decodeTable[c]));
        }

        public static int DecodeVersion(this string version) => versionDecodeTable[version];
        public static PacketType DecodeTypeId(this string packetType) => packetTypeDecodeTable[packetType];

        public static (string reminder, LiteralPacket packet) CreateLiteralPacket(this string input, int version, PacketType packetType)
        {
            var literalValue = "";
            int i;
            for (i = 0; i < input.Length - 4; i += 5)
            {
                var group = input[(i + 1)..(i + 5)];
                literalValue += group;
                if (input[i] == '0') break;
            }

            i += 5;

            return (input[i..], new LiteralPacket(version, packetType, literalValue));
        }

        public static (string reminder, IPacket packet) CreatePacket(this string input)
        {
            var version = input[0..3].DecodeVersion();
            var packetType = input[3..6].DecodeTypeId();

            if (packetType == PacketType.Literal)
            {
                var (reminder, literal) = input[6..].CreateLiteralPacket(version, packetType);
                return (reminder, literal);
            }
            else
            {
                var subPackets = new List<IPacket>();
                var totalLengthMode = input[6] == '0';
                string reminder;

                if (totalLengthMode)
                {
                    var subpacketsLength = Convert.ToInt32(input[7..22], 2);
                    reminder = input[22..(22+subpacketsLength)];
                    while (reminder.Length > 0)
                    {
                        IPacket subPacket;
                        (reminder, subPacket) = CreatePacket(reminder);
                        subPackets.Add(subPacket);
                    }
                    reminder = input[(22 + subpacketsLength)..];
                }
                else
                {
                    var numOfpackets = Convert.ToInt32(input[7..18], 2);
                    reminder = input[18..];

                    foreach (var i in Enumerable.Range(1, numOfpackets))
                    {
                        IPacket packet;
                        (reminder, packet) = CreatePacket(reminder);
                        subPackets.Add(packet);
                    }
                }

                return (reminder, new OperatorPacket(version, packetType, subPackets.ToArray()));
            }
        }
    }

    public enum PacketType
    {
        Sum,
        Product,
        Minimum,
        Maximum,
        Literal,
        GreaterThan,
        LessThan,
        EqualTo
    }

    public enum PacketLengthMode
    {
        TotalLength,
        SubPackets
    }
}
