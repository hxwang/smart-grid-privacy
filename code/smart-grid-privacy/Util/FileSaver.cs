using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace smart_grid_privacy.Util
{
    public static class FileSaver
    {

        public static void SaveListToFile(List<double> list, String path)
        {
            StreamWriter sw;
            sw = new StreamWriter(path, false);

            list.ForEach(i => sw.WriteLine(i + " "));
            sw.WriteLine();

            sw.Close();
        }
    }
}
