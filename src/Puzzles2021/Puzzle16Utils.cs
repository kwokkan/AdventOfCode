using System.Text;

namespace AdventOfCode.Puzzles2021
{
    internal class Puzzle16Utils
    {
        internal record Packet
        {
            public int Version { get; set; }

            public int TypeId { get; set; }

            public List<string> Values { get; set; } = new List<string>();

            public List<Packet> SubPackets { get; set; } = new List<Packet>();

            public int VersionSum
            {
                get
                {
                    return Version + SubPackets.Sum(x => x.VersionSum);
                }
            }

            public long LongValue
            {
                get
                {
                    var binary = string.Join(string.Empty, Values);
                    return Convert.ToInt64(binary, 2);
                }
            }

            public long ValueOf
            {
                get
                {
                    switch (TypeId)
                    {
                        case 0:
                            return SubPackets.Sum(x => x.ValueOf);
                        case 1:
                            return SubPackets.Aggregate(1L, (accumulater, source) => accumulater * source.ValueOf);
                        case 2:
                            return SubPackets.Min(x => x.ValueOf);
                        case 3:
                            return SubPackets.Max(x => x.ValueOf);
                        case 4:
                            return LongValue;
                        case 5:
                            return SubPackets[0].ValueOf > SubPackets[1].ValueOf ? 1 : 0;
                        case 6:
                            return SubPackets[0].ValueOf < SubPackets[1].ValueOf ? 1 : 0;
                        case 7:
                            return SubPackets[0].ValueOf == SubPackets[1].ValueOf ? 1 : 0;
                        default:
                            throw new InvalidOperationException($"{TypeId} not implemented.");
                    }
                }
            }
        }

        private static string InputAs(string input)
        {
            var sb = new StringBuilder(input.Length * 4);

            foreach (var current in input)
            {
                sb.Append(Convert.ToString(Convert.ToInt32(current.ToString(), 16), 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }

        private static void ParseBits(string input, Packet parentPacket, ref int fromIndex)
        {
            parentPacket.Version = Convert.ToInt32(input.Substring(fromIndex + 0, 3), 2);
            parentPacket.TypeId = Convert.ToInt32(input.Substring(fromIndex + 3, 3), 2);

            if (parentPacket.TypeId == 4)
            {
                var hasNext = true;
                var readIndex = fromIndex + 6;

                while (hasNext)
                {
                    var current = input.Substring(readIndex, 5);

                    hasNext = current[0] == '1';

                    parentPacket.Values.Add(current.Substring(1));

                    readIndex += 5;
                }

                fromIndex = readIndex;
            }
            else
            {
                var hasSubPackets = input.Substring(fromIndex + 6, 1) == "1";

                if (hasSubPackets)
                {
                    var subPacketLength = Convert.ToInt32(input.Substring(fromIndex + 7, 11), 2);

                    fromIndex += 7 + 11;

                    for (int i = 0; i < subPacketLength; i++)
                    {
                        var childPacket = new Packet();
                        parentPacket.SubPackets.Add(childPacket);
                        ParseBits(input, childPacket, ref fromIndex);
                    }
                }
                else
                {
                    var totalLength = Convert.ToInt32(input.Substring(fromIndex + 7, 15), 2);

                    fromIndex += 7 + 15;

                    var expectedend = fromIndex + totalLength;

                    while (fromIndex < expectedend)
                    {
                        var childPacket = new Packet();
                        parentPacket.SubPackets.Add(childPacket);
                        ParseBits(input, childPacket, ref fromIndex);
                    }
                }
            }
        }

        internal static Packet ParsePacket(string input)
        {
            var binaryStr = InputAs(input);
            var output = new Packet();
            var fromIndex = 0;

            ParseBits(binaryStr, output, ref fromIndex);

            return output;
        }
    }
}
