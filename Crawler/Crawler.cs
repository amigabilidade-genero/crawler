using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Crawler
{
    public abstract class Crawler
    {
        #region Propeties
        
        protected static IWebDriver _webDriver { get; set; }
        protected ChromeOptions _chromeOptions { get; set; }
        protected WebDriverWait _webDriverWait { get; set; }

        #endregion

        #region Constructor
      
        public Crawler()
        {
            InitWebDriver();
        }

        #endregion

        #region Private methods
      
        private void InitWebDriver()
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
            _webDriver = new ChromeDriver(@"C:\Users\Paulo\Documents\GitHub\crawler\Crawler\bin", options);
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Manage().Window.Maximize();
        }

        #endregion

        #region Protect methods
        protected void Url(string url)
        {
            _webDriver.Url = url;
        }

        protected bool ExistsElement(IWebDriver _webDriver, String str)
        {
            try
            {
                _webDriver.FindElement(By.XPath(str));
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