using OpenQA.Selenium;
using SharpDriver.Entities;
using SharpDriver.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Domain.Repository;
using Domain.Entities;
using Newtonsoft.Json;
using System.IO;

namespace SharpDriver.JusBrasil
{
    public class JBCrawler : Driver
    {
        #region Propeties

        private const string URL = "https://www.jusbrasil.com.br";
        private const string LOGIN_URL = "https://www.jusbrasil.com.br/login?next_url=https%3A%2F%2Fwww.jusbrasil.com.br%2Fhome";
        private string KeyWorks { get; set; }
        private List<Process> ListProcess { get; set; }
        private Process Process { get; set; }
        public JusBrasilRepository JusBrasilRepository { get; set; }
        private int BackCount = 1;

        #endregion

        #region Constructor

        public JBCrawler()  {}

        #endregion

        #region Public Methods

        public void Start(string keyWorks)
        {
            ListProcess = new List<Process>();
            KeyWorks = keyWorks;
            InitWebDriver();
            Url(URL);
            Sign();
            Thread.Sleep(3000);
            CustomSearchConfig();
            Search();
            Navigate();
            SaveProcess(ListProcess);
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
            try
            {
                if (ExistsElement("//*[contains(@data-filter-value,'next')]") || ExistsElement("//*[@class='pagination-item-link']"))
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
                    _webDriver.Quit();
                }
                else
                {
                    JusNavigate();
                    _webDriver.Quit();
                }
            }
            catch (Exception)
            {
                _webDriver.Quit();
                InitWebDriver();
                //throw e;
            }finally
            {
                _webDriver.Quit();
            }
        }

        private void JusNavigate()
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ExtractInfo()
        {
            try
            {
                var selectType = _webDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
                var itemActive = _webDriver.FindElement(By.XPath("//*[@class='JurisprudenceDecisionTabs-item btn active']"));

                if (itemActive.Text.Equals("INTEIRO TEOR"))
                {
                    selectType[0].Click();
                    BackCount += 1;
                }
                ExtractResume();
                selectType = _webDriver.FindElements(By.ClassName("JurisprudenceDecisionTabs-itemWrapper"));
                selectType[1].Click();
                BackCount += 1;
                Process.FullContent = _webDriver.FindElement(By.XPath("//*[@class='JurisprudencePage-content']")).Text;
                ListProcess.Add(Process);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ExtractResume()
        {
            try
            {
                Thread.Sleep(1000);
                var resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-title']"));
                var resumeDescription = _webDriver.FindElements(By.XPath("//*[@class='col-md-9 col-xs-12 JurisprudenceGeneralData-description']"));
                if (resumeTitle.Count == 0)
                    resumeTitle = _webDriver.FindElements(By.XPath("//*[@class='col-md-3 col-xs-12 JurisprudenceGeneralData-title']"));

                Process = new Process();
                for (int i = 0; i < resumeTitle.Count; i++)
                {
                    if (resumeTitle[i].Text == "Processo")
                    {
                        Process.RO = resumeDescription[i].Text;
                        continue;
                    }
                    else if (resumeTitle[i].Text == "Orgão Julgador")
                    {
                        Process.JudicialOrgan = resumeDescription[i].Text;
                        continue;
                    }
                    else if (resumeTitle[i].Text == "Publicação")
                    {
                        string[] temp = resumeDescription[i].Text.Split('/');
                        var date = new DateTime(Convert.ToInt16(temp[2].Split(',')[0]), Convert.ToInt16(temp[1]), Convert.ToInt16(temp[0]));
                        Process.Publication = date;
                        continue;
                    }
                    else if (resumeTitle[i].Text == "Relator")
                    {
                        Process.Reporter = resumeDescription[i].Text;
                        continue;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void SaveProcess(List<Process> listProcess)
        {
            var march = new March
            {
                DateRef = DateTime.Now,
                Subject = KeyWorks
            };
            JusBrasilRepository = new JusBrasilRepository();

            try
            {
                JusBrasilRepository.InsertMarch(march);
                int march_Id = JusBrasilRepository.GetMarchId(march.DateRef, march.Subject);
                if (listProcess.Count > 0)
                {
                    listProcess.ToList().ForEach(p => p.March_Id = march_Id);
                    JusBrasilRepository.InsertProcess(listProcess);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                JusBrasilRepository.Dispose();
            }
        }

        public void JsonWrite()
        {
            JusBrasilRepository = new JusBrasilRepository();
            var lista = JsonConvert.SerializeObject(JusBrasilRepository.GetAll());

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "JusBrasil.json")))
            {
                outputFile.WriteLine(lista);
            }
        }
        #endregion
    }
}
