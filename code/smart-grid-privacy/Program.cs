using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Simulation;
using smart_grid_privacy.Model;

namespace smart_grid_privacy
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config();
            testReadFile(config);

            Simulator sim = new Simulator(config);
            sim.runSim();
        }


        //Test Done: 4/13/2015
        public static void testReadFile(Config config) {
            Workload wl = new Workload(config);
            List<double> electricityUsage = wl.ElectricDemand;
            Console.WriteLine("Electricity count = {0}, min ={1}, max={2}, avg = {3}", electricityUsage.Count, electricityUsage.Min(), electricityUsage.Max(), electricityUsage.Average());
        }
    }
}
