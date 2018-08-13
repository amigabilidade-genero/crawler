﻿using OpenQA.Selenium;
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
        private List<Process> processProcesses { get; set; }
        private Process process { get; set; }
        private int BackCount = 1;

        public JusBrasil() { }

        public void Start()
        {
            processProcesses = new List<Process>();
            try
            {
                SearchHome();
                Sign();
                Thread.Sleep(3000);
                CustomSearchConfig();
                Search();
                JusNavigate();
                InteratorPage();
            }
            catch (Exception e)
            {
                //throw e;
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
            _webDriver.FindElement(By.XPath("//*[@class='btn btn--flat btn-login']")).Click();
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
            while (ExistsElement(_webDriver, "//*[contains(@data-filter-value,'next')]"))
            {
                _webDriver.FindElement(By.XPath("//*[contains(@data-filter-value,'next')]")).Click();
                JusNavigate();
            }
            while (ExistsElement(_webDriver, "//*[@class='pagination-item-link']"))
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
                    ExtractInfo();
                    for (int k = 0; k < BackCount; k++)
                    {
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
            processProcesses.Add(process);
        }


        private void ExtractResume()
        {
            var resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-title']"));
            var resumeDescription = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-description']"));
            if (resumeTitle.Count == 0)
                resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-3 col-xs-12 JurisprudenceGeneralData-title']"));

            process = new Process();
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

            //process = new Process();
            //if (_webDriver.FindElement(By.XPath("//*[@data-reactid='65']")).Text == "Processo")
            //    process.Id = _webDriver.FindElement(By.XPath("//*[@data-reactid='66']")).Text;
            //else if (_webDriver.FindElement(By.XPath("//*[@data-reactid='68']")).Text == "Orgão Julgador")
            //    process.OJ = _webDriver.FindElement(By.XPath("//*[@data-reactid='69']")).Text;
            //else if (_webDriver.FindElement(By.XPath("//*[@data-reactid='71']")).Text == "Publicação")
            //    process.Publication = _webDriver.FindElement(By.XPath("//*[@data-reactid='72']")).Text;
            //else if (_webDriver.FindElement(By.XPath("//*[@data-reactid='74']")).Text == "Relator")
            //    process.Publication = _webDriver.FindElement(By.XPath("//*[@data-reactid='75']")).Text;
        }
    }
}