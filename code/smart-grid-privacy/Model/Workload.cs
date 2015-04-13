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
        
        public List<double> ElectricDemand { get; private set; }
        public List<double> ExternalPower { get; private set; }


        public Workload(Config config) {
            this.ElectricDemand = new List<double>();
            this.ExternalPower = new List<double>();

            ReadWorkloadsFromFile(config);
        }


        /// <summary>
        /// check with Huahua
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Workload Clone(Config config) {
            Workload workload = new Workload(config);
            return workload;
            
        }




        public void ReadWorkloadsFromFile(Config config)
        {
            using (var sr = new StreamReader(config.InputDataFileName)) {
                this.ElectricDemand = sr.ReadListColumn(config.TimeSlotNum);
            }
        }
    }
}
