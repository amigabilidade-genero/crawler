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
            JBCrawler jBCrawler = new JBCrawler();
            jBCrawler.Start();
        }
    }
}
