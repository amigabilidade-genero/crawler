using OpenQA.Selenium;
using SharpDriver.Entities;
using SharpDriver.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDriver.JusBrasil
{
    public class JBCrawler : Driver
    {
        #region Propeties

        private const string URL = "https://www.jusbrasil.com.br";
        private const string LOGIN_URL = "https://www.jusbrasil.com.br/login?next_url=https%3A%2F%2Fwww.jusbrasil.com.br%2Fhome";
        private const string KeyWorks = "assédio sexual Belo Horizonte";
        private List<Processo> ProcessProcesses { get; set; }
        private Processo process { get; set; }
        private int BackCount = 1;

        #endregion

        #region Constructor

        public JBCrawler() { }

        #endregion

        #region Public Methods

        public void Start()
        {
            InitWebDriver();
            ProcessProcesses = new List<Processo>();
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
                _webDriver.Quit();
            }
        }

        #endregion

        #region Private Methods

        private void CustomSearchConfig()
        {
            _webDriver.FindElements(By.ClassName("navbar-link"))[3].Click();
            _webDriver.FindElement(By.Id("radio-clear")).Click();
            _webDriver.FindElements(By.ClassName("dropdown-toggle"))[2].Click();
            _webDriver.FindElement(By.Id("10000575")).Click();
        }

        private void Sign()
        {
            _webDriver.FindElement(By.XPath("//*[@class='btn btn--flat btn-login']")).Click();
            _webDriver.FindElement(By.Id("form-sign-up-email")).SendKeys("amigabilidade.genero.ifmg@gmail.com");
            _webDriver.FindElement(By.Id("form-sign-up-password")).SendKeys("@amigabilidade#genero");
            _webDriver.FindElement(By.Name("sign-up-button")).Submit();
        }

        private void Search()
        {
            Thread.Sleep(1000);
            var search = _webDriver.FindElement(By.Name("q"));
            search.SendKeys(KeyWorks);
            search.Submit();
        }

        private void Navigate()
        {
            while (ExistsElement("//*[contains(@data-filter-value,'next')]"))
            {
                _webDriver.FindElement(By.XPath("//*[contains(@data-filter-value,'next')]")).Click();
                JusNavigate();
            }
            while (ExistsElement("//*[@class='pagination-item-link']"))
            {
                _webDriver.FindElement(By.XPath("//*[contains(@aria-label,'Próximo')]")).Click();
                JusNavigate();
            }
        }

        private void JusNavigate()
        {
            Thread.Sleep(1000);
            if (ExistsElement("//*[contains(@class,'title small')]"))
            {
                var oldVersionSize = _webDriver.FindElements(By.XPath("//*[@class='title small']"));
                for (int i = 0; i < oldVersionSize.Count; i++)
                {
                    var oldV = _webDriver.FindElements(By.XPath("//*[@class='title small']"));
                    oldV[i].FindElement(By.TagName("a")).Click();
                    ExtractInfo();
                    for (int k = 0; k < BackCount; k++)
                    {
                        Thread.Sleep(1000);
                        _webDriver.Navigate().Back();
                    }
                    BackCount = 1;
                }
            }
            else
            {
                var newVersion = _webDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                for (int i = 0; i < newVersion.Count; i++)
                {
                    var newV = _webDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                    newV[i].FindElement(By.TagName("a")).Click();
                    ExtractInfo();
                    for (int k = 0; k < BackCount; k++)
                    {
                        Thread.Sleep(1000);
                        _webDriver.Navigate().Back();
                    }
                    BackCount = 1;
                }
            }
        }

        private void ExtractInfo()
        {
            var selectType = _webDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
            if (!selectType[0].Selected)
            {
                selectType[0].Click();
                BackCount += 1;
            }
            ExtractResume();
            selectType = _webDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
            selectType[1].Click();
            BackCount += 1;
            process.FullContent = _webDriver.FindElement(By.XPath("//*[@class='JurisprudencePage-content']")).Text;
            ProcessProcesses.Add(process);
            string jsonObject = ToJsonString(process);
        }

        private void ExtractResume()
        {
            Thread.Sleep(1000);
            var resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-title']"));
            var resumeDescription = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-description']"));
            if (resumeTitle.Count == 0)
                resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-3 col-xs-12 JurisprudenceGeneralData-title']"));

            process = new Processo();
            for (int i = 0; i < resumeTitle.Count; i++)
            {
                if (resumeTitle[i].Text == "Processo")
                {
                    process.RO = resumeDescription[i].Text;
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
