using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smart_grid_privacy.Util;

namespace smart_grid_privacy.Algorithm
{
    public class NILL: AlgBase
    {
        public double[] power; //low, stable, high
        public double stateLow {get; private set;}
        public double stateHigh { get; private set; }
        public double stateLowToStable { get; private set; }
        public enum State {low, stable, high}
        public State CurrentState;
        public double Threshold { get; private set; }
        public double Alpha { get; private set; }
        public int LastRecoverTime { get; private set; }

        public NILL() {
            this.AlgType = AlgType.NILL;
            init();
        }

        public void init() {
            this.stateLow = 0.2*this.Battery.Capacity;
            this.stateHigh = 0.9 * this.Battery.Capacity;
            this.stateLowToStable = 0.8 * this.Battery.Capacity;
            this.CurrentState = State.low;
            power[(int)(State.low)] = this.Battery.MaximumChargeRate;
            this.Alpha = 0.5;
            this.Threshold = this.Battery.MaximumChargeRate*0.1;
        }

        public override void DecideEnergy(int time)
        {
            double demandPower = this.Workload.ElectricDemand[0];
            double batteryPower = 0;
            if (time == 0)
            {
                batteryPower = this.Battery.MaximumChargeRate;
            }
            else {
                SwitchState(demandPower, time);
                if (CurrentState == State.stable) {
                    batteryPower = power[(int)(State.stable)] - demandPower;
                    if (batteryPower < this.Battery.CurrMaximumDisChargeRate) batteryPower = this.Battery.CurrMaximumDisChargeRate;
                    if (batteryPower > this.Battery.currMaximumChargeRate) batteryPower = this.Battery.currMaximumChargeRate;
                }
                else if (CurrentState == State.low) {
                    batteryPower = this.Battery.MaximumChargeRate;
                    if (batteryPower > this.Battery.currMaximumChargeRate) batteryPower = this.Battery.currMaximumChargeRate;
                    if (batteryPower < -1e-3)
                    {
                        throw new Exception("Should Charge Battery!");
                    }
                }
                else if (CurrentState == State.high) {
                    UpdateHighStatePower(demandPower);
                    batteryPower = (power[(int)(State.high)] - demandPower);
                    if (batteryPower > 1e-3) {
                        throw new Exception("Should Discharge Battery!");
                    }
                }
            }

            Battery.ChargeDisChargeBattery(batteryPower);
            Battery.UpdateBattery(batteryPower);
            Workload.ExternalPower.Add(demandPower + batteryPower); 
        }


        public void SwitchState(double demand, int time ) {
            if (CurrentState == State.low && this.Battery.CurrentLevel > this.stateLowToStable)
            {
                CurrentState = State.stable;
                UpdateStablePower(time);
            }
            else if(CurrentState == State.stable && this.Battery.CurrentLevel < this.stateLow &&demand > power[(int)(State.stable)]){
                CurrentState = State.low;
                LastRecoverTime = time;
            }
            else if (CurrentState == State.stable && this.Battery.CurrentLevel > this.stateHigh && demand < power[(int)(State.stable)])
            {
                CurrentState = State.high;
                LastRecoverTime = time;
                UpdateHighStatePower(demand);
            }
            else if ( (CurrentState == State.high) && (demand - power[(int)(State.high)])>Threshold){
                CurrentState = State.stable;
                UpdateStablePower(time);
            }
        
        }

        /// <summary>
        /// update stablePower using exponential weighted average
        /// </summary>
        /// <param name="time"></param>
        public void UpdateStablePower(int time) {
            var histAvgPower = this.Workload.ElectricDemand.GetRange(this.LastRecoverTime, time).Average();
            this.power[(int)(State.stable)] = this.Alpha * histAvgPower + (1 - this.Alpha) * power[(int)(State.stable)];
        }

        public void UpdateHighStatePower(double demand) {
            this.power[(int)(State.high)] = Math.Max(0, demand - RandomGenerator.GetRandomInt(1,5)*this.Threshold/5); 
        }

    }
}
