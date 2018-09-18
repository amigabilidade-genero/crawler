using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Process
    {
        public string RO { get; set; }
        public string JudicialOrgan { get; set; }
        public DateTime Publication { get; set; }
        public string Reporter { get; set; }
        public string FullContent { get; set; }
        public int March_Id { get; set; }
    }
}
