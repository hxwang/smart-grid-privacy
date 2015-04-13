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

        public Config() {
         
            this.TimeSlotLength = 5; //each time slot has 5 minutes
            this.Capacity = 500; //0.5 kWh, used by NILL paper
            this.MaximumChargeRate = 1000; //1000w/h, used by NILL paper
            this.MaximumDischargeRate = 1000; //1000w/h, used by NILL paper
        }
    }
}
