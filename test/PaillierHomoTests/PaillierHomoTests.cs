using System;
using System.Numerics;
using Xunit;
using Aprismatic.Paillier.Homomorphism;

namespace PaillierHomoTests
{
    public class PaillierHomoTests
    {
        [Fact(DisplayName = "Addition - encoded")]
        public void TestAddition()
        {
            byte[] firstPos = { 1 };
            byte[] firstNeg = { 2 };
            byte[] secondPos = { 3 };
            byte[] secondNeg = { 4 };
            byte[] NSquare = { 10 };
            byte[] expected = {3, 8};

            var res = new byte[firstPos.Length * 2].AsSpan();
            PaillierHomomorphism.AddEncoded(firstPos, firstNeg, secondPos, secondNeg, NSquare, res[..firstPos.Length], res[firstPos.Length..]);

            Assert.Equal(expected, res.ToArray());
        }

        [Fact(DisplayName = "Subtraction - encoded")]
        public void TestSubtraction()
        {
            byte[] firstPos = { 1 };
            byte[] firstNeg = { 2 };
            byte[] secondPos = { 3 };
            byte[] secondNeg = { 4 };
            byte[] NSquare = { 10 };
            byte[] expected = { 4, 6 };

            var res = new byte[firstPos.Length * 2].AsSpan();
            PaillierHomomorphism.SubtractEncoded(firstPos, firstNeg, secondPos, secondNeg, NSquare, res[..firstPos.Length], res[firstPos.Length..]);

            Assert.Equal(expected, res.ToArray());
        }

        [Fact(DisplayName = "Addition - low-level, bytes")]
        public void TestAdditionLowLevelBytes()
        {
            byte[] first = { 5 };
            byte[] second = { 3 };
            byte[] NSquare = { 10 };
            byte[] expected = { 5 };

            var res = new byte[first.Length].AsSpan();
            PaillierHomomorphism.AddIntegers(first, second, NSquare, res);

            Assert.Equal(expected, res.ToArray());
        }

        [Fact(DisplayName = "Addition - low-level, BI")]
        public void TestAdditionLowLevelBI()
        {
            var first = new BigInteger(7);
            var second = new BigInteger(4);
            var NSquare = new BigInteger(10);
            var expected = new BigInteger(8);

            var res = PaillierHomomorphism.AddIntegers(first, second, NSquare);

            Assert.Equal(expected, res);
        }
    }
}
