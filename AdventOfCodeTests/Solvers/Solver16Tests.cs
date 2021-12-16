using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AdventOfCodeTests.Solvers
{
    [TestClass()]
    public class Solver16Tests
    {
        [DataTestMethod]
        [DataRow("D2FE28", 6)]
        [DataRow("38006F45291200", 9)]
        [DataRow("EE00D40C823060", 14)]
        [DataRow("8A004A801A8002F478", 16)]
        [DataRow("620080001611562C8802118E34", 12)]
        [DataRow("C0015000016115A2E0802F182340", 23)]
        [DataRow("A0016C880162017C3686B18A3D4780", 31)]
        public void Should_Get_Sum_Of_Versions(string input, int expected)
        {
            var decoded = input.Decode();

            var (reminder, packet) = decoded.CreatePacket();

            var version = packet.GetVersion();

            Assert.AreEqual(expected, version);
        }

        [DataTestMethod]
        [DataRow("C200B40A82", 3)]
        [DataRow("04005AC33890", 54)]
        [DataRow("880086C3E88112", 7)]
        [DataRow("CE00C43D881120", 9)]
        [DataRow("D8005AC2A8F0", 1)]
        [DataRow("F600BC2D8F", 0)]
        [DataRow("9C005AC2F8F0", 0)]
        [DataRow("9C0141080250320F1802104A08", 1)]
        public void Should_Get_Total_Value(string input, int expected)
        {
            var decoded = input.Decode();

            var (reminder, packet) = decoded.CreatePacket();

            var version = packet.GetValue();

            Assert.AreEqual(expected, version);
        }
    }
}
