using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy
{
    public class Config
    {
        public double TimeSlotLength { get; private set; }
        public double Capacity { get; set; }
        public double MaximumChargeRate { get; private set; }
        public double MaximumDischargeRate { get; private set; }

        public int SimDays { get; private set; } //simulation days


        public String InputDataFileName {get; private set;}
        public String OutputDir { get; private set; }

        public int TimeSlotNum
        {
            get
            {
                return SimDays * 24 * 12;
            }
        }

        public Config() {
         
            this.TimeSlotLength = 5; //each time slot has 5 minutes
            this.Capacity = 0.5; //0.5 kWh, used by NILL paper
            this.MaximumChargeRate = 500; //1000w/h (by NILL paper), 500w/h (by BE paper)
            this.MaximumDischargeRate = -500; //1000w/h, used by NILL paper
            this.SimDays = 3; 
            this.InputDataFileName = @"..\..\..\..\data\3day_5min.txt";
            this.OutputDir = @"..\..\..\..\data\simOutput\";
        }
    }
}
