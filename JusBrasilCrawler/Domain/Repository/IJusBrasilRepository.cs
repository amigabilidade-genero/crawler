using Domain.DataTranferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IJusBrasilRepository
    {
        void InsertMarch(March march);
        void InsertProcess(Process process);
        int GetMarchId(DateTime dateRef, string subject);
        IEnumerable<March> GetAllMarch();
        IEnumerable<March> GetMarchBySubject(string subject);
        IEnumerable<March> GetMarchByDateRef(DateTime dateRef);
        IEnumerable<Process> GetAllProcess();
        IEnumerable<Process> GetProcessBySubject(string subject);
        IEnumerable<Process> GetProcessByDateRef(DateTime dateRef);
        IEnumerable<DTOJusBrasilProcess> GetAll();
    }
}
