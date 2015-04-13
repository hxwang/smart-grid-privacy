using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Algorithm
{
    public class NILL: AlgBase
    {

        public NILL() {
            this.AlgType = AlgType.NILL;
        }

        public override void DecideEnergy(int time)
        {
            throw new NotImplementedException();
        }
    }
}
