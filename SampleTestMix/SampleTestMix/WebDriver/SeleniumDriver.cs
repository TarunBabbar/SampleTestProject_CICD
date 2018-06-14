using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTestMix.WebDriver
{
    public sealed class SeleniumDriver
    {
        private static SeleniumDriver instance = null;

        private SeleniumDriver()
        {

        }

        public static SeleniumDriver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SeleniumDriver();
                }
                return instance;
            }
        }

        private IWebDriver _seleniumWebDriver;

        public IWebDriver SeleniumWebDriver
        {
            get
            {
                return _seleniumWebDriver;
            }
            set
            {
                _seleniumWebDriver = value;
            }
        }


        public void InitializeWebBrowser(string deploymentDirectory)
        {
            switch (ConfigurationManager.AppSettings["BrowserName"])
            {
                case "headlesschrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments(new List<string>() { "--headless", "--disable-gpu", "--window-size=1920x1080" });
                    _seleniumWebDriver = new ChromeDriver(deploymentDirectory, options);
                    break;

                case "chrome":
                    defaultLabel: ChromeOptions chromeoptions = new ChromeOptions();
                    chromeoptions.AddArguments(new List<string>() { "no-sandbox", "test-type", "--disable-extensions", "disable-infobars" });
                    _seleniumWebDriver = new ChromeDriver(deploymentDirectory, chromeoptions);
                    _seleniumWebDriver.Manage().Window.Maximize();
                    break;

                case "firefox":
                    FirefoxDriverService service =
                           FirefoxDriverService.CreateDefaultService(deploymentDirectory,
                               "geckodriver.exe");
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments(new List<string>() { "--disable-extensions", "disable-infobars" });
                    _seleniumWebDriver = new FirefoxDriver(service, firefoxOptions, new TimeSpan(hours: 0, minutes: 0, seconds: 60));
                    break;

                case "headlessfirefox":
                    FirefoxDriverService headlessservice =
                           FirefoxDriverService.CreateDefaultService(deploymentDirectory,
                               "geckodriver.exe");
                    FirefoxOptions firefoxOptionsHeadless = new FirefoxOptions();
                    firefoxOptionsHeadless.AddArguments(new List<string>() { "--headless", "--disable-gpu", "--window-size=1920x1080" });
                    _seleniumWebDriver = new FirefoxDriver(headlessservice, firefoxOptionsHeadless, new TimeSpan(hours: 0, minutes: 0, seconds: 60));
                    break;

                case "ie":
                    InternetExplorerOptions opt = new InternetExplorerOptions
                    {
                        EnableNativeEvents = false,
                        RequireWindowFocus = true,
                        IgnoreZoomLevel = true,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnsureCleanSession = true

                    };

                    opt.AddAdditionalCapability("browserstack.ie.enablePopups", "true");
                    _seleniumWebDriver = new InternetExplorerDriver(deploymentDirectory, opt, new TimeSpan(hours: 0, minutes: 0, seconds: 60));
                    _seleniumWebDriver.Manage().Window.Maximize();
                    break;

                case "edge":
                    EdgeDriverService driverService = EdgeDriverService.CreateDefaultService(deploymentDirectory, "MicrosoftWebDriver.exe");
                    EdgeOptions edgeOptions = new EdgeOptions()
                    {
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };

                    _seleniumWebDriver = new EdgeDriver(driverService, edgeOptions);
                    break;

                default:
                    goto defaultLabel;
            }

            _seleniumWebDriver.Manage().Cookies.DeleteAllCookies();
            _seleniumWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }
    }
}
