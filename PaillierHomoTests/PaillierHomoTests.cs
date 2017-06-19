using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaillierExt.Homomorphism;

namespace PaillierHomoTests
{
    [TestClass]
    public class PaillierHomoTests
    {
        [TestMethod]
        public void TestZero()
        {
            byte[] p_first = { 0x01 };
            byte[] p_second = { 0x01 };
            byte[] p_NSquare = { 0x01 };
            byte[] expected = { 0x00 };

            var res = PaillierHomomorphism.Addition(p_first, p_second, p_NSquare);

            Assert.AreEqual(expected[0], res[0]);
        }
    }
}
