using System.Security.Cryptography;
using AdventOfCode.PuzzleCore;

namespace AdventOfCode.Puzzles2015;

public class Puzzle4 : PuzzleBase
{
    private class Hasher
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public Hasher()
        {
            _hashAlgorithm = MD5.Create();
        }

        ~Hasher()
        {
            _hashAlgorithm.Dispose();
        }

        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = _hashAlgorithm.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }

    public Puzzle4()
        : base(2015, 4)
    {
    }

    private static long Calculate(string input, string prefix)
    {
        var count = 1;
        var hasher = new Hasher();

        while (true)
        {
            var hashed = hasher.CreateMD5(input + count);

            if (hashed.StartsWith(prefix))
            {
                break;
            }

            count++;
        }

        return count;
    }

    private long Calculate1(string input)
    {
        return Calculate(input, "00000");
    }

    private static long Calculate2(string input)
    {
        return Calculate(input, "000000");
    }

    public override long ExpectedSample1 => 1048970;
    public override long Sample1()
    {
        var input = GetSampleString()[0];

        return Calculate1(input);
    }

    public override long ExpectedSample2 => 5714438;
    public override long Sample2()
    {
        var input = GetSampleString()[0];

        return Calculate2(input);
    }

    public override long ExpectedAnswer1 => 346386;
    public override long Solve1()
    {
        var input = GetInputString()[0];

        return Calculate1(input);
    }

    public override long ExpectedAnswer2 => 9958218;
    public override long Solve2()
    {
        var input = GetInputString()[0];

        return Calculate2(input);
    }
}
