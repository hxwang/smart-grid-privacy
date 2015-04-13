using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Model
{
    public class Battery
    {
        public double Capacity { get; set; }
        public double MaximumChargeRate { get; private set; }
        public double MaximumDischargeRate { get; private set; }


        public double MaximumChargeAmount
        {
            get
            {
                return ConvertToKWH(MaximumChargeRate);
            }
        }

        public double MaximumDisChargeAmount
        {
            get
            {
                return ConvertToKWH(MaximumDischargeRate);
            }
        }

        public double CurrentLevel { get; set; }

        public void ChargeBattery(double amount) {
            this.CurrentLevel += amount;
            if (this.CurrentLevel - this.Capacity > 1e-3)
                throw new ArgumentOutOfRangeException("Battery overflow!");
            
        }


        public Battery Clone() {
            throw new NotImplementedException();
        }

        public void DisChargeBattery(double amount) {
            this.CurrentLevel -= amount;
            if(this.CurrentLevel - this.Capacity < -1e-3)
                throw new ArgumentOutOfRangeException("Battery over discharge!");
        }

        /// <summary>
        /// convert per unit to kWh
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public double ConvertToKWH(double power) {

            return power * 5 / 60 / 1000;
        }


        public double MaximumToCharge() { 
            return Math.Max(MaximumChargeAmount, this.Capacity - this.CurrentLevel);
        }

        public double MaximumToDisCharge(){
            return Math.Max(MaximumDisChargeAmount, this.CurrentLevel);
        }

        public Battery(Config config) {
            this.Capacity = config.Capacity;
            this.MaximumChargeRate = config.MaximumChargeRate;
            this.MaximumDischargeRate = config.MaximumDischargeRate;
        }
    }
}
