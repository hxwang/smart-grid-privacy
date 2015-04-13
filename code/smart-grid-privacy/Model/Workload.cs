using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using smart_grid_privacy.Util;

namespace smart_grid_privacy.Model
{
    public class Workload
    {
        private Config Config;
        public List<double> ElectricDemand { get; private set; }
        public List<double> ExternalDemand { get; private set; }


        public Workload(Config config) {
            this.ElectricDemand = new List<double>();
            this.ExternalDemand = new List<double>();
            this.Config = config;
            init();
        }


        public Workload Clone() {

            throw new NotImplementedException();
        }

        public void init() {
            ReadWorkloadsFromFile();       
        }


        public void ReadWorkloadsFromFile() {
            using (var sr = new StreamReader(Config.InputDataFileName)) {
                this.ElectricDemand = sr.ReadListColumn(Config.TimeSlotNum);
            }
        }
    }
}
