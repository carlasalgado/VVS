using System;
using NUnit.Framework;

namespace TestNUnit
{
    [TestFixture]
    public class NUnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
    }
}