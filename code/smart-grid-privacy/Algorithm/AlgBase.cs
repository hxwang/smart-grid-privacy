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

        public Workload Workload { get; set; }
        public Battery Battery { get; set; }

        public AlgType AlgType { get; protected set; }
        public double LastExternalWorkload { get; private set; }



        /// <summary>
        /// decide how much energy to extract from grid
        /// decide how much energy to charge/discharge from battery
        /// update external energy, and battery state accordingly
        /// </summary>
        /// <param name="time"></param>
        public abstract void DecideEnergy(int time);



        /// <summary>
        /// save the results to file
        /// note: both energy demandPower and external energy are required to be saved, will be used to compute mutual information
        /// </summary>
        public void SaveFile(Config config)
        {
            FileSaver.SaveListToFile(Workload.ElectricDemand, config.OutputDir  + AlgType + "_elecDemand.txt");
            FileSaver.SaveListToFile(Workload.ExternalPower, config.OutputDir  + AlgType + "_extEnergy.txt");
            FileSaver.SaveListToFile(Battery.BatteryEnergyLevelList, config.OutputDir + AlgType + "_batteryEnergyListHist.txt");
            FileSaver.SaveListToFile(Battery.BattertChargePowerList, config.OutputDir  + AlgType + "_batteryPowerHist.txt");
        }


    }
    }

