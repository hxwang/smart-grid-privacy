using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Model
{
    public class Workload
    {
        List<double> ElectricDemand { get; private set; }
        List<double> ExternalDemand { get; private set; }


        public Workload() {
            this.ElectricDemand = new List<double>();
            this.ExternalDemand = new List<double>();
        }

        public void ReadWorkloadsFromFile(String fileName) {
            throw new NotImplementedException("Not implemented");
        
        }
    }
}
