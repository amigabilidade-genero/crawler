using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class March
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateRef { get; set; }
        public string Subject { get; set; }
        public List<Process> ListProcess { get; set; }
        public March()
        {
            ListProcess = new List<Process>();
        }
    }
}
