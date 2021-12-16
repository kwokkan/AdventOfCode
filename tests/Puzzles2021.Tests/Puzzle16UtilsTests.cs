using Xunit;
using static AdventOfCode.Puzzles2021.Puzzle16Utils;

namespace AdventOfCode.Puzzles2021.Tests
{
    public class Puzzle16UtilsTests
    {
        [Theory]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void VersionSum(string input, int expected)
        {
            // Arrange
            var output = ParsePacket(input);

            // Act
            var result = output.VersionSum;

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("C200B40A82", 3)]                  // finds the sum of 1 and 2, resulting in the value 3
        [InlineData("04005AC33890", 54)]               // finds the product of 6 and 9, resulting in the value 54
        [InlineData("880086C3E88112", 7)]              // finds the minimum of 7, 8, and 9, resulting in the value 7
        [InlineData("CE00C43D881120", 9)]              // finds the maximum of 7, 8, and 9, resulting in the value 9
        [InlineData("D8005AC2A8F0", 1)]                // produces 1, because 5 is less than 15
        [InlineData("F600BC2D8F", 0)]                  // produces 0, because 5 is not greater than 15
        [InlineData("9C005AC2F8F0", 0)]                // produces 0, because 5 is not equal to 15
        [InlineData("9C0141080250320F1802104A08", 1)]  // produces 1, because 1 + 3 = 2 * 2
        public void ValueOf(string input, int expected)
        {
            // Arrange
            var output = ParsePacket(input);

            // Act
            var result = output.ValueOf;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
