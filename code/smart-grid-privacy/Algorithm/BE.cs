using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Algorithm
{
    public class BE: AlgBase
    {
        public BE() {
            this.AlgType = AlgType.BE;
        }

        public override void DecideEnergy(int t)
        {
            double demandPower = this.Workload.ElectricDemand[t];

            double batteryPower = 0;
            if (t == 0)
            {

                batteryPower = 0;
            }
            else { 
                double lastExternalPower = this.Workload.ExternalPower[t-1];
                //discharge battery
                if (demandPower - lastExternalPower > 0)
                {
                    double diff = lastExternalPower - demandPower;
                    double maxDischargeRate = Battery.CurrMaximumDisChargeRate;
                    
                    batteryPower = Math.Max(maxDischargeRate, diff);
                    //Console.WriteLine("Discharge: Diff = {0}, maxDischargeRate = {1}, batteryPower = {2}", diff, maxDischargeRate, batteryPower);
                    if (batteryPower > 1e-3) {
                        throw new Exception("Battery shoud discharge!");
                    }
                    
                }
                else { 
                //charge battery
                    double diff = lastExternalPower - demandPower;
                    double maxChargeRate = Battery.currMaximumChargeRate;
                    batteryPower = Math.Min(maxChargeRate, diff);
                    //Console.WriteLine("Charge: Diff = {0}, maxDischargeRate = {1}, batteryPower = {2}", diff, maxChargeRate, batteryPower);
                    if (batteryPower < -1e-3)
                    {
                        throw new Exception("Battery shoud charge!");
                    }
                }
            }


            Battery.ChargeDisChargeBattery(batteryPower);
            Battery.UpdateBattery(batteryPower);
            Workload.ExternalPower.Add(demandPower + batteryPower); 
        }

        public void UpdateParameter(int t) { 
        
        
        }
    }
}
