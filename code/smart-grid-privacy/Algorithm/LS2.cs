using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Util;

namespace smart_grid_privacy.Algorithm
{
    public class LS2: LS1
    {
       

        public LS2() {
            this.AlgType = AlgType.LS2;
        }


        /// <summary>
        /// random choose charging/discharging when cannot maintain the same load level
        /// </summary>
        /// <param name="time"></param>
        public override void DecideEnergy(int time)
        {
            double demandPower = this.Workload.ElectricDemand[time];
            double batteryPower = 0;
            int ceil = (int)Math.Ceiling(demandPower / BatteryParameter);
            int floor = (int)Math.Floor(demandPower / BatteryParameter);
            if (time == 0)
            {
                IsCharging = true;
            }
            else
            {

                double diff = (Multiple) * BatteryParameter - demandPower;

                //maintaining the same states would result in battery overflow
                if (diff > this.Battery.currMaximumChargeRate || ceil * BatteryParameter - demandPower > this.Battery.currMaximumChargeRate || this.Battery.CurrentLevel > this.highEnergyLine) IsCharging = false;
                else if (diff < this.Battery.CurrMaximumDisChargeRate || floor * BatteryParameter - demandPower < this.Battery.CurrMaximumDisChargeRate || this.Battery.CurrentLevel < this.lowEnergyLine) IsCharging = true;
                //the powerDemand is either too low or too high to maintain the same states
                else if (demandPower <= (Multiple - 1) * BatteryParameter || demandPower >= (Multiple + 1) * BatteryParameter)
                {
                    if (this.Battery.CurrentLevel < (this.lowEnergyLine + this.highEnergyLine) / 2) IsCharging = true;
                    else IsCharging = false;
                }
                else if (diff > 0) IsCharging = true;
                else IsCharging = false;

            }

            if (IsCharging)
            {
                Multiple = ceil;
            }
            else
            {
                Multiple = floor;
            }

            batteryPower = Multiple * BatteryParameter - demandPower;

            //check with Huahua, in this case, battery still has chance to overflow
            if (batteryPower > this.Battery.currMaximumChargeRate || batteryPower < this.Battery.CurrMaximumDisChargeRate)
            {

                Console.WriteLine("BatteryPower = {0}, Min={1}, Max ={2}, demandPower ={3}, multiple ={4}", batteryPower, this.Battery.CurrMaximumDisChargeRate, this.Battery.currMaximumChargeRate, demandPower, Multiple);
                throw new Exception("battery should not charge/discharge at such high rate");

            }

            Battery.ChargeDisChargeBattery(batteryPower);
            Battery.UpdateBattery(batteryPower);
            Workload.ExternalPower.Add(demandPower + batteryPower);

        }
    }
}
