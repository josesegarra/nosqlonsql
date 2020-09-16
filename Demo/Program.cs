using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No input files to execute. Using default...");
                args = new string[] { "demo\\test01.nosql", "demo\\test02.nosql", "demo\\test03.nosql" };
            }
            var dbConnection = ConfigurationManager.AppSettings["dbconnection"];
            Console.WriteLine("Using dabatase:  " + dbConnection);
            using (var n = new SqlConnection(dbConnection))
            {
                n.Open();
                for (var i = 0; i < args.Length; i++)
                {
                    if (System.IO.File.Exists(args[i])) Database.ExecuteNoSql(n,args[i]); else Utils.Red("File not found: " + args[i]);
                }
            }
        }
    }
}
