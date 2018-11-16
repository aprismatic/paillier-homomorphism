using System;
using System.Numerics;

namespace Aprismatic.PaillierExt.Homomorphism
{
    public static class PaillierHomomorphism
    {
        public static byte[] Addition(byte[] p_first, byte[] p_second, byte[] p_NSquare)
        {
            var p_first_actual = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first_actual, p_first.Length / 2);
            var p_first_negative = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first.Length / 2, p_first_negative, 0, p_first.Length / 2);
            var p_second_actual = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second_actual, p_second.Length / 2);
            var p_second_negative = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second.Length / 2, p_second_negative, 0, p_second.Length / 2);

            var add_actual = AddParts(p_first_actual, p_second_actual, p_NSquare);
            var add_negative = AddParts(p_first_negative, p_second_negative, p_NSquare);

            //p_first.Length is equals to p_second.Length
            var add = new byte[p_first.Length];
            Array.Copy(add_actual, 0, add, 0, add_actual.Length);
            Array.Copy(add_negative, 0, add, add.Length / 2, add_negative.Length);

            return add;
        }

        public static byte[] Subtraction(byte[] p_first, byte[] p_second, byte[] p_NSquare)
        {
            var p_first_actual = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first_actual, p_first.Length / 2);
            var p_first_negative = new byte[p_first.Length / 2];
            Array.Copy(p_first, p_first.Length / 2, p_first_negative, 0, p_first.Length / 2);
            var p_second_actual = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second_actual, p_second.Length / 2);
            var p_second_negative = new byte[p_second.Length / 2];
            Array.Copy(p_second, p_second.Length / 2, p_second_negative, 0, p_second.Length / 2);

            var add_actual = AddParts(p_first_actual, p_second_negative, p_NSquare);
            var add_negative = AddParts(p_first_negative, p_second_actual, p_NSquare);

            var sub = new byte[p_first.Length];
            Array.Copy(add_actual, 0, sub, 0, add_actual.Length);
            Array.Copy(add_negative, 0, sub, sub.Length / 2, add_negative.Length);

            return sub;
        }

        private static byte[] AddParts(byte[] p_first, byte[] p_second, byte[] p_NSquare)
        {
            var A = new BigInteger(p_first);
            var B = new BigInteger(p_second);
            var NSquare = new BigInteger(p_NSquare);

            var bi_res = (A * B) % NSquare;
            var res = bi_res.ToByteArray();
            return res;
        }
    }
}
