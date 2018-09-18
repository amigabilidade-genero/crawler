using Domain.SqlBase;
using SharpDriver.JusBrasil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusBrasilCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> KeyWords = new List<string>();
            //KeyWords.Add("assédio sexual");
            KeyWords.Add("assédio moral 2017");
            //KeyWords.Add("assédio tecnologia");
            //KeyWords.Add("assédio informática");
     
            JBCrawler jBCrawler = new JBCrawler();
            foreach (var key in KeyWords)
            {
                jBCrawler.Start(key);
            }
            jBCrawler.JsonWrite();
        }
    }
}
