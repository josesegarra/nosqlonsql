using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Table
    {
        int[] align;
        int[] sizes;
        int[] psizes;
        internal string[] Headers;
        string margin = new String(' ', 5);


        internal Table()
        {
            align = new int[0];
            sizes = align;
            psizes = align;
            Headers = new string[0];
        }


        internal Table(string[] columns)
        {
            Headers = columns;
            sizes = new int[Headers.Length];
            psizes = new int[Headers.Length];
            align = new int[Headers.Length];
            for (var i = 0; i < sizes.Length; i++) sizes[i] = Headers[i].Length;
            for (var i = 0; i < sizes.Length; i++) align[i] = 0;
        }
        internal List<string[]> rows = new List<string[]>();
        internal void Add(object[] l)
        {
            var cells = new string[Headers.Length];
            for (var i = 0; i < l.Length; i++)
            {
                cells[i] = l[i] == null ? "null" : l[i].ToString();
                if (cells[i].Length > sizes[i]) sizes[i] = cells[i].Length;
                if (align[i] == 0)
                {
                    if (l[i].GetType() == typeof(string)) align[i] = 1; else if (l[i].GetType() != typeof(System.DBNull)) align[i] = 2;
                }
            }
            rows.Add(cells);
        }


        internal void Prepare(int maxsize)
        {
            int k = sizes.Sum();                                                            // Sum of all sizes
            if (k < maxsize) { psizes = sizes.Select(x => x).ToArray(); return; }                 // If everything fits then do nothing
            for (var i = 0; i < sizes.Length; i++)                                                 // Update proportinaly
            {
                var x = sizes[i] * maxsize / k;
                psizes[i] = (x < 5) ? 6 : x;
            }
        }
        internal string[] Format(string[] k)
        {
            string[] l = new string[Headers.Length];
            for (var i = 0; i < k.Length; i++)
            {
                var s = k[i];
                if (s.Length > 5 && s.Length > psizes[i]) s = s.Substring(0, psizes[i] - 2) + "..";
                l[i] = align[i] == 2 ? s.PadLeft(psizes[i]) : s.PadRight(psizes[i]);
            }
            return l;
        }

        internal string[] Row(int i) => (i < 0 || i >= rows.Count) ? null : rows[i];

        void TableLine(int? number,string[] items, ConsoleColor c)
        {
            if (!number.HasValue) Utils.White(margin); else Utils.DarkBlue((" " + number.Value).PadRight(margin.Length));

            for (var n = 0; n < items.Length; n++)
            {
                Utils.Print(items[n], c);
                if (n < (items.Length - 1)) Utils.Print(" | ", ConsoleColor.DarkGray); else Utils.Print(" ",ConsoleColor.White,true);
            }
        }


        internal void Print()
        {
            if (Headers.Length==0)
            {
                Utils.White("---No data ",true);
                return;
            }
            Prepare(120);                                                                                               // Let's create a buffer of 120 characters width
            var line = Format(Headers);                                                                                 // Guess sizes for each column based on headers
            Utils.White(" ", true);
            TableLine(null,line, ConsoleColor.Cyan);
            Utils.White(margin);
            Utils.DarkGray(new String('-', line.Sum(x => 3 + x.Length) - 3),true);
            int i = 0;
            while (Row(i) != null)
            {
                TableLine(i,Format(Row(i)), (i % 2 == 0) ? ConsoleColor.DarkCyan : ConsoleColor.Blue);
                i++;
            }
        }

    }
}
