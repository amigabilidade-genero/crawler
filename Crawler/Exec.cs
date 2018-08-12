using Crawler.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler
{
    class Exec
    {
        static void Main(string[] args)
        {
            JusBrasil jusBrasil = new JusBrasil();
            jusBrasil.Start();
        }
    }
}
