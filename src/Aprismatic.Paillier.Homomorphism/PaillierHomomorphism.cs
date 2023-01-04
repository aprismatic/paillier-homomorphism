using System;
using System.Numerics;

namespace Aprismatic.Paillier.Homomorphism
{
    /// <summary>
    /// This class provides low-level methods for homomorphic operations on Paillier ciphertexts.
    /// </summary>
    public static class PaillierHomomorphism
    {
        /// <summary>
        /// Adds two encoded (i.e., supporting negatives through 2's complement) Paillier ciphertexts (in the form of byte arrays).
        /// </summary>
        /// <param name="firstActual">First ciphertext, the 'original sign' half</param>
        /// <param name="firstNegative">First ciphertext, the 'negated sign' half</param>
        /// <param name="secondActual">Second ciphertext, the 'original sign' half</param>
        /// <param name="secondNegative">Second ciphertext, the 'negated sign' half</param>
        /// <param name="NSquare">Part of the public key that is required for computation: N²</param>
        /// <param name="resultActual">Span to write the 'original sign' part of the result</param>
        /// <param name="resultNegative">Span to write the 'negated sign' part of the result</param>
        public static void AddEncoded(
            ReadOnlySpan<byte> firstActual, ReadOnlySpan<byte> firstNegative,
            ReadOnlySpan<byte> secondActual, ReadOnlySpan<byte> secondNegative,
            ReadOnlySpan<byte> NSquare,
            Span<byte> resultActual, Span<byte> resultNegative)
        {
            var firstActualBI = new BigInteger(firstActual);
            var firstNegativeBI = new BigInteger(firstNegative);
            var secondActualBI = new BigInteger(secondActual);
            var secondNegativeBI = new BigInteger(secondNegative);
            var NSquareBI = new BigInteger(NSquare);

            var (resActualBI, resNegativeBI) = AddEncoded(firstActualBI, firstNegativeBI, secondActualBI, secondNegativeBI, NSquareBI);

            resActualBI.TryWriteBytes(resultActual, out _);
            resNegativeBI.TryWriteBytes(resultNegative, out _);
        }

        /// <summary>
        /// Adds two encoded (i.e., supporting negatives through 2's complement) Paillier ciphertexts (in the form of <see cref="BigInteger"/>s).
        /// </summary>
        /// <param name="firstActual">First ciphertext, the 'original sign' encryption</param>
        /// <param name="firstNegative">First ciphertext, the 'negated sign' encryption</param>
        /// <param name="secondActual">Second ciphertext, the 'original sign' encryption</param>
        /// <param name="secondNegative">Second ciphertext, the 'negated sign' encryption</param>
        /// <param name="NSquare">Part of the public key that is required for computation: N²</param>
        /// <returns>A tuple of two BigInteger's containing an encryption of sum of originals and an encryption of sum of negated</returns>
        public static (BigInteger, BigInteger) AddEncoded(
            BigInteger firstActual, BigInteger firstNegative,
            BigInteger secondActual, BigInteger secondNegative,
            BigInteger NSquare)
        {
            var actual = AddIntegers(firstActual, secondActual, NSquare);
            var negative = AddIntegers(firstNegative, secondNegative, NSquare);
            return (actual, negative);
        }

        /// <summary>
        /// Subtracts 2-complement encoded Paillier ciphertext `second` from `first` in the form of byte arrays.
        /// </summary>
        /// <param name="firstActual">First ciphertext, the 'original sign' half</param>
        /// <param name="firstNegative">First ciphertext, the 'negated sign' half</param>
        /// <param name="secondActual">Second ciphertext, the 'original sign' half</param>
        /// <param name="secondNegative">Second ciphertext, the 'negated sign' half</param>
        /// <param name="NSquare">Part of the public key that is required for computation: N²</param>
        /// <param name="resultActual">Span to write the 'original sign' part of the result</param>
        /// <param name="resultNegative">Span to write the 'negated sign' part of the result</param>
        public static void SubtractEncoded(
            ReadOnlySpan<byte> firstActual, ReadOnlySpan<byte> firstNegative,
            ReadOnlySpan<byte> secondActual, ReadOnlySpan<byte> secondNegative,
            ReadOnlySpan<byte> NSquare,
            Span<byte> resultActual, Span<byte> resultNegative)
        {
            var firstActualBI = new BigInteger(firstActual);
            var firstNegativeBI = new BigInteger(firstNegative);
            var secondActualBI = new BigInteger(secondActual);
            var secondNegativeBI = new BigInteger(secondNegative);
            var NSquareBI = new BigInteger(NSquare);

            var (resActualBI, resNegativeBI) = SubtractEncoded(firstActualBI, firstNegativeBI, secondActualBI, secondNegativeBI, NSquareBI);

            resActualBI.TryWriteBytes(resultActual, out _);
            resNegativeBI.TryWriteBytes(resultNegative, out _);
        }

        /// <summary>
        /// Subtracts 2-complement encoded Paillier ciphertext `second` from `first` in the form of <see cref="BigInteger"/>s.
        /// </summary>
        /// <param name="firstActual">First ciphertext, the 'original sign' encryption</param>
        /// <param name="firstNegative">First ciphertext, the 'negated sign' encryption</param>
        /// <param name="secondActual">Second ciphertext, the 'original sign' encryption</param>
        /// <param name="secondNegative">Second ciphertext, the 'negated sign' encryption</param>
        /// <param name="NSquare">Part of the public key that is required for computation: N²</param>
        /// <returns>A tuple of two BigInteger's containing an encryption (A-B) and an encryption of (-A+B)</returns>
        public static (BigInteger, BigInteger) SubtractEncoded(
            BigInteger firstActual, BigInteger firstNegative,
            BigInteger secondActual, BigInteger secondNegative,
            BigInteger NSquare)
        {
            var actual = AddIntegers(firstActual, secondNegative, NSquare);
            var negative = AddIntegers(firstNegative, secondActual, NSquare);
            return (actual, negative);
        }

        /// <summary>
        /// Low-level adds two Paillier-encrypted integers in byte-array form, not aware of the 2-complement or any other encoding.
        /// </summary>
        /// <param name="first">First ciphertext</param>
        /// <param name="second">Second ciphertext</param>
        /// <param name="NSquare">Public key N²</param>
        /// <param name="result">Span to write the result to</param>
        public static void AddIntegers(
            ReadOnlySpan<byte> first, ReadOnlySpan<byte> second,
            ReadOnlySpan<byte> NSquare,
            Span<byte> result)
        {
            var A = new BigInteger(first);
            var B = new BigInteger(second);
            var NSquareBi = new BigInteger(NSquare);

            var resBi = AddIntegers(A, B, NSquareBi);
            resBi.TryWriteBytes(result, out _);
        }

        /// <summary>
        /// Low-level adds two Paillier-encrypted integers in BigInteger form, not aware of the 2-complement or any other encoding.
        /// </summary>
        /// <param name="first">First ciphertext</param>
        /// <param name="second">Second ciphertext</param>
        /// <param name="NSquare">Public key N²</param>
        /// <returns>BigInteger containing ciphertext of homomorphic sum of `first` and `second`</returns>
        public static BigInteger AddIntegers(
            BigInteger A, BigInteger B,
            BigInteger NSquare)
        {
            return (A * B) % NSquare;
        }
    }
}
