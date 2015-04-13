using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Algorithm;
using smart_grid_privacy.Model;
using smart_grid_privacy.Util;

namespace smart_grid_privacy.Algorithm
{
    public abstract class AlgBase
    {
        public Workload Workload {get;  set;}
        public Battery Battery {get;  set;}

        public AlgType AlgType { get; protected set; }
        public double LastExternalWorkload { get; private set; }

       /// <summary>
       /// decide how much energy to extract from grid
       /// decide how much energy to charge/discharge from battery
       /// update external energy, and battery state accordingly
       /// </summary>
       /// <param name="time"></param>
        public void DecideEnergy(int time) {
            throw new NotImplementedException();
        }


        /// <summary>
        /// save the results to file
        /// note: both energy demand and external energy are required to be saved, will be used to compute mutual information
        /// </summary>
        public void SaveFile() {
            throw new NotImplementedException();
        }



    }
}
