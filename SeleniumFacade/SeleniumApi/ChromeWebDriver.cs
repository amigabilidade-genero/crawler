using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JusBrasilFacade.SeleniumApi
{
    public abstract class ChromeWebDriver
    {
        #region Propeties

        protected static IWebDriver _chromeWebDriver { get; set; }
        protected ChromeOptions _chromeOptions { get; set; }

        #endregion

        #region Constructor

        public ChromeWebDriver() {   }

        #endregion

        #region Private methods

        protected void InitWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArguments(
                "test-type",
                "start-maximized",
                "--js-flags=--expose-gc",
                "--enable-precise-memory-info",
                "--disable-popup-blocking",
                "--disable-default-apps",
                "test-type=browser",
                "disable-infobars",
                "--disable-extensions",
                "--incognito"
                );
            _chromeWebDriver = new ChromeDriver(@"C:\Users\Paulo\Documents\GitHub\crawler\Crawler\bin", options);
            _chromeWebDriver.Manage().Cookies.DeleteAllCookies();
            _chromeWebDriver.Manage().Window.Maximize();
        }

        #endregion

        #region Protect methods
        protected void Url(string url)
        {
            _chromeWebDriver.Url = url;
        }

        protected bool ExistsElement(String str)
        {
            try
            {
                _chromeWebDriver.FindElement(By.XPath(str));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}