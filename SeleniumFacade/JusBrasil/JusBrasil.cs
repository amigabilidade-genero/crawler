using JusBrasilFacade.SeleniumApi;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JusBrasilFacade.JusBrasil
{
    public class JusBrasil : ChromeWebDriver
    {
        #region Propeties
        private const string URL = "https://www.jusbrasil.com.br";
        private const string LOGIN_URL = "https://www.jusbrasil.com.br/login?next_url=https%3A%2F%2Fwww.jusbrasil.com.br%2Fhome";
        private const string KeyWorks = "assédio sexual Belo Horizonte";
        private List<Processo> processProcesses { get; set; }
        private Processo process { get; set; }
        private int BackCount = 1;

        #endregion

        #region Constructor

        public JusBrasil() {}

        #endregion

        #region Public Methods

        public void Start()
        {
            InitWebDriver();
            processProcesses = new List<Processo>();
            try
            {
                Url(URL);
                Sign();
                Thread.Sleep(3000);
                CustomSearchConfig();
                Search();
                Navigate();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _chromeWebDriver.Quit();
            }
        }

        #endregion

        #region Private Methods

        private void CustomSearchConfig()
        {
            _chromeWebDriver.FindElements(By.ClassName("navbar-link"))[3].Click();
            _chromeWebDriver.FindElement(By.Id("radio-clear")).Click();
            _chromeWebDriver.FindElements(By.ClassName("dropdown-toggle"))[2].Click();
            _chromeWebDriver.FindElement(By.Id("10000575")).Click();
        }

        private void Sign()
        {
            _chromeWebDriver.FindElement(By.XPath("//*[@class='btn btn--flat btn-login']")).Click();
            _chromeWebDriver.FindElement(By.Id("form-sign-up-email")).SendKeys("amigabilidade.genero.ifmg@gmail.com");
            _chromeWebDriver.FindElement(By.Id("form-sign-up-password")).SendKeys("@amigabilidade#genero");
            _chromeWebDriver.FindElement(By.Name("sign-up-button")).Submit();
        }

        private void Search()
        {
            var search = _chromeWebDriver.FindElement(By.Name("q"));
            search.SendKeys(KeyWorks);
            search.Submit();
        }

        private void Navigate()
        {
            while (ExistsElement("//*[contains(@data-filter-value,'next')]"))
            {
                _chromeWebDriver.FindElement(By.XPath("//*[contains(@data-filter-value,'next')]")).Click();
                JusNavigate();
            }
            while (ExistsElement("//*[@class='pagination-item-link']"))
            {
                _chromeWebDriver.FindElement(By.XPath("//*[contains(@aria-label,'Próximo')]")).Click();
                JusNavigate();
            }
        }

        private void JusNavigate()
        {
            if (ExistsElement( "//*[contains(@class,'title small')]"))
            {
                var oldVersionSize = _chromeWebDriver.FindElements(By.XPath("//*[@class='title small']"));
                for (int i = 0; i < oldVersionSize.Count; i++)
                {
                    var oldV = _chromeWebDriver.FindElements(By.XPath("//*[@class='title small']"));
                    oldV[i].FindElement(By.TagName("a")).Click();
                    ExtractInfo();
                    for (int k = 0; k < BackCount; k++)
                    {
                        _chromeWebDriver.Navigate().Back();
                    }
                    BackCount = 1;
                }
            }
            else
            {
                var newVersion = _chromeWebDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                for (int i = 0; i < newVersion.Count; i++)
                {
                    var newV = _chromeWebDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                    newV[i].FindElement(By.TagName("a")).Click();
                    ExtractInfo();
                    for (int k = 0; k < BackCount; k++)
                    {
                        _chromeWebDriver.Navigate().Back();
                    }
                    BackCount = 1;
                }
            }
        }

        private void ExtractInfo()
        {
            var selectType = _chromeWebDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
            if (!selectType[0].Selected)
            {
                selectType[0].Click();
                BackCount += 1;
            }

            ExtractResume();
            selectType = _chromeWebDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
            selectType[1].Click();
            BackCount += 1;
            process.FullContent = _chromeWebDriver.FindElement(By.XPath("//*[@class='JurisprudencePage-content']")).Text;
            processProcesses.Add(process);
        }

        private void ExtractResume()
        {
            var resumeTitle = _chromeWebDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-title']"));
            var resumeDescription = _chromeWebDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-description']"));
            if (resumeTitle.Count == 0)
                resumeTitle = _chromeWebDriver.FindElements(By.XPath("//*[@class='col-md-3 col-xs-12 JurisprudenceGeneralData-title']"));

            process = new Processo();
            for (int i = 0; i < resumeTitle.Count; i++)
            {
                if (resumeTitle[i].Text == "Processo")
                {
                    process.Id = resumeDescription[i].Text;
                    continue;
                }
                else if (resumeTitle[i].Text == "Orgão Julgador")
                {
                    process.OJ = resumeDescription[i].Text;
                    continue;
                }
                else if (resumeTitle[i].Text == "Publicação")
                {
                    process.Publication = resumeDescription[i].Text;
                    continue;
                }
                else if (resumeTitle[i].Text == "Relator")
                {
                    process.Reporter = resumeDescription[i].Text;
                    continue;
                }
            }
        }

        #endregion
    }
}
