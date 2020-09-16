using System;
using System.Linq;
using System.Data.SqlClient;
using NoSqlOnSql;

namespace Demo
{
    class Database
    {
        public static void ExecuteNoSql(SqlConnection database, string fileName)
        {
            Utils.Yellow("Executing " + fileName, true);
            var source = System.IO.File.ReadAllText(fileName);
            DisplayMessages(source);
            var nosql = new NoSql();                                                               // Create Compiler
            if (!nosql.Transpile(source+"  ", "mssql"))
            {
                Utils.Red(nosql.Error);
                return;
            }
            for (var i = 0; i < nosql.Code.Length; i++)
                if (nosql.Code[i].Type == BlockType.Code)
                    ExecuteSql(database, nosql.Code[i].Value);
            Utils.Yellow(" ");
        }

        static void ExecuteSql(SqlConnection database,string code)
        {
            using (SqlCommand command = new SqlCommand(code, database))
            {
                using (var rs = command.ExecuteReader())
                {
                    do FetchResuls(rs).Print(); while (rs.NextResult());
                }
            }
        }

        static void DisplayMessages(string l)
        {
            foreach (var s in l.Split('\n').Where(x => x.StartsWith("-- MSG ")))
                Utils.Gray(s.Substring(6).Trim(), true);
        }

        static Table FetchResuls(SqlDataReader rs)
        {
            if (!rs.HasRows) return new Table();                                   
            object[] values = new object[rs.FieldCount];
            Table table = new Table(Enumerable.Range(0, rs.FieldCount).Select(x => rs.GetName(x)).ToArray());
            int k = 100;
            while (k > 0 && rs.Read()) { rs.GetValues(values); table.Add(values); k--; }
            return table;
        }

    }
}
