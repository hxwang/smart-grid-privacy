using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smart_grid_privacy.Util
{
    public static class RandomGenerator
    {


        public static Random Instance;


        public static int GetRandomInt(int low, int high) {
            if(Instance==null)
                Instance = new Random();
            return Instance.Next(low, high);
        }
    }
}
