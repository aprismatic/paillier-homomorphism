using Xunit;
using Aprismatic.PaillierExt.Homomorphism;

namespace PaillierHomoTests
{
    public class PaillierHomoTests
    {
        [Fact(DisplayName = "Addition")]
        public void TestAddition()
        {
            byte[] first = { 1, 2 };
            byte[] second = { 3, 4 };
            byte[] NSquare = { 10 };
            byte[] expected = { 3, 8 };

            var res = PaillierHomomorphism.Addition(first, second, NSquare);

            Assert.Equal(expected, res);
        }

        [Fact(DisplayName = "Subtraction")]
        public void TestSubtraction()
        {
            byte[] first = { 1, 2 };
            byte[] second = { 3, 4 };
            byte[] NSquare = { 10 };
            byte[] expected = { 4, 6 };

            var res = PaillierHomomorphism.Subtraction(first, second, NSquare);

            Assert.Equal(expected, res);
        }
    }
}
