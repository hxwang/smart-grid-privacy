using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Util;

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

        public override void Init() {
            this.BatteryParameter = Math.Min(this.Battery.MaximumChargeRate, -this.Battery.MaximumDischargeRate);
          
        }

        public override void DecideEnergy(int time)
        {
            double demandPower = this.Workload.ElectricDemand[time];
            double batteryPower = 0;
            if (time == 0)
            {
                IsCharging = true;
            }
            else {
                
                double diff = (Multiple) * BatteryParameter - demandPower;
                //maintaining the same states would result in battery overflow
                if (diff > this.Battery.currMaximumChargeRate) IsCharging = false;
                else if (diff < this.Battery.CurrMaximumDisChargeRate) IsCharging = true;
                //the powerDemand is either too low or too high to maintain the same states
                else if (demandPower <= (Multiple - 1) * BatteryParameter || demandPower >= (Multiple + 1) * BatteryParameter)
                {
                    double rnd = RandomGenerator.GetRandomDouble();
                    if (rnd < 0.5) IsCharging = true;
                    else IsCharging = false;
                }

            }

            if (IsCharging)
            {
                Multiple = (int)Math.Ceiling(demandPower / BatteryParameter);
            }
            else {
                Multiple = (int)Math.Floor(demandPower / BatteryParameter);
            }

            batteryPower = Multiple * BatteryParameter - demandPower;

            //check with Huahua, in this case, battery still has chance to overflow
            if (batteryPower > this.Battery.currMaximumChargeRate || batteryPower < this.Battery.CurrMaximumDisChargeRate)
            {
                if (batteryPower > this.Battery.currMaximumChargeRate) {
                    Multiple--;
                    batteryPower = Multiple * BatteryParameter - demandPower;
                }

                if (batteryPower > this.Battery.currMaximumChargeRate || batteryPower < this.Battery.CurrMaximumDisChargeRate)
                {
                    Console.WriteLine("BatteryPower = {0}, Min={1}, Max ={2}, demandPower ={3}, multiple ={4}", batteryPower, this.Battery.CurrMaximumDisChargeRate, this.Battery.currMaximumChargeRate, demandPower, Multiple);
                    throw new Exception("battery should not charge/discharge at such high rate");
                }

            }
            Battery.ChargeDisChargeBattery(batteryPower);
            Battery.UpdateBattery(batteryPower);
            Workload.ExternalPower.Add(demandPower + batteryPower); 

        }
    }
}
