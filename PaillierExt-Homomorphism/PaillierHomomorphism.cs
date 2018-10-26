using System;
using System.Numerics;

namespace Aprismatic.PaillierExt.Homomorphism
{
    public static class PaillierHomomorphism
    {
        public static byte[] Addition(byte[] first, byte[] second, byte[] NSquare)
        {
            var firstActual = new byte[first.Length / 2];
            Array.Copy(first, firstActual, first.Length / 2);
            var firstNegative = new byte[first.Length / 2];
            Array.Copy(first, first.Length / 2, firstNegative, 0, first.Length / 2);
            var secondActual = new byte[second.Length / 2];
            Array.Copy(second, secondActual, second.Length / 2);
            var secondNegative = new byte[second.Length / 2];
            Array.Copy(second, second.Length / 2, secondNegative, 0, second.Length / 2);

            var addActual = AddParts(firstActual, secondActual, NSquare);
            var addNegative = AddParts(firstNegative, secondNegative, NSquare);

            var add = new byte[first.Length];
            Array.Copy(addActual, 0, add, 0, addActual.Length);
            Array.Copy(addNegative, 0, add, add.Length / 2, addNegative.Length);

            return add;
        }

        public static byte[] Subtraction(byte[] first, byte[] second, byte[] NSquare)
        {
            var firstActual = new byte[first.Length / 2];
            Array.Copy(first, firstActual, first.Length / 2);
            var firstNegative = new byte[first.Length / 2];
            Array.Copy(first, first.Length / 2, firstNegative, 0, first.Length / 2);
            var secondActual = new byte[second.Length / 2];
            Array.Copy(second, secondActual, second.Length / 2);
            var secondNegative = new byte[second.Length / 2];
            Array.Copy(second, second.Length / 2, secondNegative, 0, second.Length / 2);

            var subActual = AddParts(firstActual, secondNegative, NSquare);
            var subNegative = AddParts(firstNegative, secondActual, NSquare);

            var sub = new byte[first.Length];
            Array.Copy(subActual, 0, sub, 0, subActual.Length);
            Array.Copy(subNegative, 0, sub, sub.Length / 2, subNegative.Length);

            return sub;
        }

        private static byte[] AddParts(byte[] first, byte[] second, byte[] NSquare)
        {
            var A = new BigInteger(first);
            var B = new BigInteger(second);
            var NSquareBi = new BigInteger(NSquare);

            var resBi = (A * B) % NSquareBi;
            var res = resBi.ToByteArray();
            return res;
        }
    }
}
