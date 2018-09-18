using Dapper;
using Domain.DataTranferObject;
using Domain.Entities;
using Domain.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class JusBrasilRepository : IJusBrasilRepository
    {
        private readonly IDbConnection _db;

        public JusBrasilRepository()
        {
            _db = Base.GetInstance().GetConexao();
        }

        public void Dispose()
        {
            _db.Close();
        }

        public void InsertMarch(March march)
        {
            var param = new
            {
                DateRef = march.DateRef.ToString("yyyy-MM-dd"),
                Subject = march.Subject
            };

            var queryString = "INSERT INTO March (DateRef, Subject) VALUES (@DateRef, @Subject)";
            _db.Query<March>(queryString, param);
        }

        public IEnumerable<March> GetAllMarch()
        {
            var queryString = "SELECT * FROM March";
            return _db.Query<March>(queryString);
        }

        public IEnumerable<March> GetMarchBySubject(string subject)
        {
            var param = new
            {
                Subject = subject
            };
            var queryString = "SELECT * FROM March  WHERE Subject = @Subject";
            return _db.Query<March>(queryString, param);
        }

        public IEnumerable<March> GetMarchByDateRef(DateTime dateRef)
        {
            var param = new
            {
                DateRef = dateRef.ToString("yyyy-MM-dd"),
            };
            var queryString = "SELECT * FROM March WHERE DateRef = @DateRef";
            return _db.Query<March>(queryString, param);
        }

        public int GetMarchId(DateTime dateRef, string subject)
        {
            var param = new
            {
                DateRef = dateRef.ToString("yyyy-MM-dd"),
                Subject = subject
            };
            var queryString = "SELECT March.Id FROM March WHERE March.DateRef = @DateRef AND March.Subject = @Subject";
            return _db.QuerySingle<int>(queryString, param);
        }

        public void InsertProcess(Process process)
        {
            var param = new
            {
                RO = process.RO,
                JudicialOrgan = process.JudicialOrgan,
                Publication = process.Publication,
                Reporter = process.Reporter,
                FullContent = process.FullContent,
                March_Id = process.March_Id
            };

            var queryString = "INSERT INTO Process (RO, JudicialOrgan, Publication, Reporter, FullContent, March_Id) " +
                "VALUES (@RO, @JudicialOrgan, @Publication, @Reporter, @FullContent, @March_Id)";
            _db.Query<March>(queryString, param);
        }

        public void InsertProcess(List<Process> process)
        {
            var queryString = "INSERT INTO Process (RO, JudicialOrgan, Publication, Reporter, FullContent, March_Id) " +
                "VALUES (@RO, @JudicialOrgan, @Publication, @Reporter, @FullContent, @March_Id)";
            _db.Execute(queryString, process);
        }

        public IEnumerable<Process> GetAllProcess()
        {
            var queryString = "SELECT * FROM Process";
            return _db.Query<Process>(queryString);
        }

        public IEnumerable<Process> GetProcessBySubject(string subject)
        {
            var param = new
            {
                Subject = subject
            };
            var queryString = "SELECT * FROM Process INNER JOIN March ON March.Id = Process.March_Id WHERE March.Subject = @Subject";
            return _db.Query<Process>(queryString, param);
        }

        public IEnumerable<Process> GetProcessByDateRef(DateTime dateRef)
        {
            var param = new
            {
                DateRef = DateTime.Parse(dateRef.ToString()).ToString("yyyy/MM/dd")
            };
            var queryString = "SELECT * FROM Process INNER JOIN March ON March.Id = Process.March_Id WHERE March.DateRef = @DateRef";
            return _db.Query<Process>(queryString, param);
        }

        public IEnumerable<DTOJusBrasilProcess> GetAll()
        {
            List<DTOJusBrasilProcess> dTOJusBrasilProcesses = new List<DTOJusBrasilProcess>();
            var listMarchs = GetAllMarch();
            var listProcess = GetAllProcess();
            foreach (var march in listMarchs)
            {
                var dtoMarch = new DTOJusBrasilProcess
                {
                    March = march
                };
                foreach (var proc in listProcess)
                {
                    if (proc.March_Id == dtoMarch.March.Id)
                        dtoMarch.March.ListProcess.Add(proc);
                }
                dTOJusBrasilProcesses.Add(dtoMarch);
            }

            return dTOJusBrasilProcesses;
        }
    }
}
