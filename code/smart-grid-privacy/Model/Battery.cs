using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Model
{
    public class Battery
    {
        public double TimeSlotLength { get; private set; }
        public double Capacity { get; set; }
        public double MaximumChargeRate { get; private set; }
        public double MaximumDischargeRate { get; private set; }
        public List<double> BatteryEnergyLevelList { get; set; }
        public List<double> BattertChargePowerList { get; set; }
        public double CurrentLevel { get; set; }

        /// <summary>
        /// unit: w/s
        /// </summary>
        public double CurrMaximumDisChargeRate {
            get {
                double maxRateConstraintedByAvailableEnergy = -(this.CurrentLevel * 3600 / (this.TimeSlotLength * 60) * 1000);
                double rnt =  Math.Max(maxRateConstraintedByAvailableEnergy, this.MaximumDischargeRate);
                if (rnt > 1e-3) {
                   // Console.WriteLine("CurrentLevel = {0}, maxRateConstraintedByAvailableEnergy ={1}, MaximumDischargeRate={2}",CurrentLevel,  maxRateConstraintedByAvailableEnergy, MaximumDischargeRate);
                    throw new Exception("Battery should discahrge!");
                }
                return rnt;
            }
        }

        /// <summary>
        /// ybut: w/s
        /// </summary>
        public double currMaximumChargeRate{

            get
            {
                double rnt=  Math.Min((this.Capacity-this.CurrentLevel) * 3600 / (this.TimeSlotLength * 60) * 1000, this.MaximumChargeRate);
                if (rnt < -1e-3)
                {
                    throw new Exception("Battery should charge!");
                }
                return rnt;
            }
        }



       

        /// <summary>
        /// charge/discharge battery
        /// charge: amount positive
        /// discharge: amount negative
        /// </summary>
        /// <param name="amount"></param>
        public void ChargeDisChargeBattery(double power) {
            
            double amount = ConvertToKWH(power);
            this.CurrentLevel += amount;
            //Console.WriteLine("CurrentLevel = {0}", CurrentLevel);
            if (this.CurrentLevel - this.Capacity > 1e-3)
            {
               
                throw new ArgumentOutOfRangeException("Battery overflow! Charge Failed!");
            }
            if (this.CurrentLevel < -1e-3)
            {
               // Console.WriteLine("CurrentBatteryEnergyLevel = {0}", this.CurrentLevel);
                throw new ArgumentOutOfRangeException("Battery over discharge!");
            }
            
        }

        public void UpdateBattery(double power) {
            BattertChargePowerList.Add(power);
            BatteryEnergyLevelList.Add(this.CurrentLevel);
        }


        

       

        /// <summary>
        /// convert per unit to kWh
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public double ConvertToKWH(double power)
        {

            double rnt =  power * this.TimeSlotLength * 60 / 1000 / 3600;
            return rnt;
        }


   

        public Battery(Config config) {
            this.Capacity = config.Capacity;
            this.MaximumChargeRate = config.MaximumChargeRate;
            this.MaximumDischargeRate = config.MaximumDischargeRate;
            this.BatteryEnergyLevelList = new List<double>();
            this.BattertChargePowerList = new List<double>();
            this.TimeSlotLength = config.TimeSlotLength;
            this.CurrentLevel = 0;
        }

        //check with Huahua
        public Battery Clone(Config config)
        {
            Battery bat = new Battery(config);
            return bat;
        }
    }
}
