using Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SqlBase
{
   public class Base
    {
        public static readonly Base instanceMySQL = new Base();
        public Base() { }

        public static Base GetInstance()
        {
            return instanceMySQL;
        }

        public MySqlConnection GetConexao()
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            return new MySqlConnection(conn);
        }
    }
}
