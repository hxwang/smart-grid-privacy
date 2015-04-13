﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Model;
using smart_grid_privacy.Algorithm;
using smart_grid_privacy.Util;


namespace smart_grid_privacy.Simulation
{
    public class Simulator
    {
        Config Config;
        public Simulator(Config config) {
            this.Config = config;
        }

        public void runSim() {
            Workload workload = new Workload(this.Config);
            Battery bat = new Battery(this.Config);
            foreach (String algType in Enum.GetNames(typeof(AlgType)))
            {
                var Alg = AlgFactory.CreateAlg(algType);
                Alg.Workload = workload.Clone();
                Alg.Battery = bat.Clone();

                //TODO: for each time slot,  run the algorithm to decide the assignment of energy
                for (int i = 0; i < Config.TimeSlotNum; i++)
                    Alg.DecideEnergy(i);

                Alg.SaveFile();
                
            }

        }

    }
}