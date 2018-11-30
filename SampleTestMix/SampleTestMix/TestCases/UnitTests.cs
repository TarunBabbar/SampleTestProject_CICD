using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleTestMix.TestCases
{
    [TestClass]
    public class UnitTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void VerifyAdditionOfTwoString()
        {
            TestContext.WriteLine("Started Test");
            ("Invoice" + "Smash").Should().Be("InvoiceSmash");
            TestContext.WriteLine("Ended Test");
        }

        [TestMethod, TestCategory("Failed-UnitTest")]
        public void VerifyFailingOfAdditionOfTwoString()
        {
            TestContext.WriteLine("Started Test");
            ("Invoice" + "Smash").Should().Be("Invoice");
            TestContext.WriteLine("Ended Test");
        }

        
        [TestMethod, TestCategory("UnitTest")]
        public void VerifyAdditionOfTwoNumbers()
        {
            TestContext.WriteLine("Started Test");
            (1 + 2).Should().Be(3);
            TestContext.WriteLine("Ended Test");
        }

    }
}
