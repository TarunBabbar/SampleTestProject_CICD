using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTestMix.TestCases
{
    [TestClass]
    public class UITests : BaseUITest
    {
        [TestMethod, TestCategory("UI")]
        public void Sample_NavigateToGoogle()
        {
            TestContext.WriteLine("Started Test");
            SeleniumDriver.Navigate().GoToUrl("http://google.com/");
            TestContext.WriteLine("Ended Test");
        }

        [TestMethod, TestCategory("UI")]
        public void Sample_NavigateToTOI()
        {
            TestContext.WriteLine("Started Test");
            SeleniumDriver.Navigate().GoToUrl("https://timesofindia.indiatimes.com/");
            TestContext.WriteLine("Ended Test");
        }
    }
}
