using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SampleTestMix.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver = SampleTestMix.WebDriver.SeleniumDriver;

namespace SampleTestMix.TestCases
{
    [TestClass]
    public abstract class BaseUITest
    {
        private static IWebDriver seleniumDriver;
        private static TestContext testContextInstance;


        [AssemblyInitialize]
        public static void AssemblySetUp(TestContext context)
        {
            Driver.Instance.InitializeWebBrowser(context.DeploymentDirectory);
            testContextInstance = context;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            SeleniumDriver.Quit();
        }

        protected static IWebDriver SeleniumDriver
        {
            get
            {
                return Driver.Instance.SeleniumWebDriver;
            }
            set
            {
                seleniumDriver = Driver.Instance.SeleniumWebDriver;
            }
        }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [TestInitialize]
        public void TestInitialize()
        {
            TestContext = testContextInstance;
        }
        [TestCleanup]
        public void TestCleanUp()
        {
            TestContext.WriteLine("Entered Test Clean Up");
            //if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            //{
            string name = TestContext.DeploymentDirectory + "\\" + TestContext.TestName + ".png";
            ((ITakesScreenshot)SeleniumDriver).GetScreenshot().SaveAsFile(name, ScreenshotImageFormat.Png);
            TestContext.AddResultFile(name);
            //}

            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                var type = Type.GetType(TestContext.FullyQualifiedTestClassName);
                if (type != null)
                {
                    var instance = Activator.CreateInstance(type);
                    var method = type.GetMethod(TestContext.TestName);
                    //int retries = 2;
                    //while (true)
                    //{
                        try
                        {
                            method.Invoke(instance, null);
                        }
                        catch
                        {
                            //if (--retries == 0)
                            throw;
                            //else System.Threading.Thread.Sleep(1000);
                        }
                    //}
                }
            }

        }
    }
}
