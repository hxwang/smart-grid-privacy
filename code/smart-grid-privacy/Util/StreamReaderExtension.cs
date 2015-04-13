using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace smart_grid_privacy.Util
{
    public static class StreamReaderExtension
    {
        public static List<double> ReadListPerLine(this StreamReader self, int count)
        {
            var line = self.ReadLine();
            var items = line.Split(' ');
            var list = new List<Double>();
            foreach (String s in items)
            {
                list.Add(double.Parse(s));
                if (list.Count == count) break;
            }

            return list;

        }

        public static List<double> ReadListColumn(this StreamReader self, int count)
        {
            var list = new List<Double>();
            while (!self.EndOfStream)
            {
                var line = self.ReadLine();
                list.Add(double.Parse(line));
                if (list.Count == count) break;
            }

            return list;
        }

    }
}
