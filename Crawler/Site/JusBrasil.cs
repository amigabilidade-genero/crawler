using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Site
{
    public class JusBrasil : Crawler
    {
        private const string URL = "https://www.jusbrasil.com.br";
        private const string LOGIN_URL = "https://www.jusbrasil.com.br/login?next_url=https%3A%2F%2Fwww.jusbrasil.com.br%2Fhome";
        private const string KeyWorks = "assédio sexual Belo Horizonte";
        private List<string> listDocuments;

        public JusBrasil() { }


        public void Start()
        {
            listDocuments = new List<string>();
            try
            {
                SearchHome();
                //Sign();
                CustomSearchConfig();
                Search();  
                JusNavigate();
                InteratorPage();
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

        private void SearchHome()
        {
            Url(URL);
            //Url(LOGIN_URL);
        }

        private void CustomSearchConfig()
        {
            _webDriver.FindElements(By.ClassName("navbar-link"))[3].Click();
            _webDriver.FindElement(By.Id("radio-clear")).Click();
            _webDriver.FindElements(By.ClassName("dropdown-toggle"))[2].Click();
            _webDriver.FindElement(By.Id("10000575")).Click();
        }

        private void Sign()
        {
            _webDriver.FindElement(By.Id("form-sign-up-email")).SendKeys("amigabilidade.genero.ifmg@gmail.com");
            _webDriver.FindElement(By.Id("form-sign-up-password")).SendKeys("@amigabilidade#genero");
            _webDriver.FindElement(By.Name("sign-up-button")).Submit();
        }

        private void Search()
        {
            var search = _webDriver.FindElement(By.Name("q"));
            search.SendKeys(KeyWorks);
            search.Submit();
        }

        private void InteratorPage()
        {
            while(ExistsElement(_webDriver, "//*[contains(@data-filter-value,'next')]"))
            {
                _webDriver.FindElement(By.XPath("//*[contains(@data-filter-value,'next')]")).Click();
                JusNavigate();
            }
            while(ExistsElement(_webDriver, "//*[@class='pagination-item-link']"))
            {
                _webDriver.FindElement(By.XPath("//*[contains(@aria-label,'Próximo')]")).Click();
                JusNavigate();
            }
        }

        private void JusNavigate()
        {
            if (ExistsElement(_webDriver, "//*[contains(@class,'title small')]"))
            {
                var oldVersionSize = _webDriver.FindElements(By.XPath("//*[@class='title small']"));
                for (int i = 0; i < oldVersionSize.Count; i++)
                {
                    var oldV = _webDriver.FindElements(By.XPath("//*[@class='title small']"));
                    oldV[i].FindElement(By.TagName("a")).Click();
                    SelectInteiroTeor();
                    GetText();
                    _webDriver.Navigate().Back();
                }
            }
            else
            {
                var newVersion = _webDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                for (int i = 0; i < newVersion.Count; i++)
                {
                    var newV = _webDriver.FindElements(By.ClassName("BaseSnippetWrapper-title"));
                    newV[i].FindElement(By.TagName("a")).Click();
                    SelectInteiroTeor();
                    GetText();
                    _webDriver.Navigate().Back();
                }
            }  
        }

        private void GetText()
        {
            var textContent = _webDriver.FindElement(By.XPath("//*[@class='JurisprudencePage-content anon-content']"));
            listDocuments.Add(textContent.Text);
        }

        private void SelectInteiroTeor()
        {
            var inteiroTeor = _webDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
            if (!inteiroTeor[1].Selected)
                inteiroTeor[1].Click();
        }
    }
}
