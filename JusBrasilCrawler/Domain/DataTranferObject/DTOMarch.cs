using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTranferObject
{
    public class DTOMarch
    {
        public DateTime DateRef { get; set; }
        public string Subject { get; set; }
        public List<DTOProcess> ListProcess { get; set; }
    }
}
