using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Algorithm
{
    public class LS2: AlgBase
    {
        public double BatteryParameter;
        public int Multiple;
        public double lowEnergyLine;
        public double highEnergyLine;
        public Boolean IsCharging;
        public enum State { Low, Mid, High}


        public LS2() {
            this.AlgType = AlgType.LS2;
        }

        public void init() {
            this.BatteryParameter = Math.Min(this.Battery.MaximumChargeRate, -this.Battery.MaximumDischargeRate);
        }
        public override void DecideEnergy(int time)
        {
            double batteryPower = 0;
            if (time == 0)
            {
                batteryPower = 0;
            }
            else { 
                
            }
        }
    }
}
