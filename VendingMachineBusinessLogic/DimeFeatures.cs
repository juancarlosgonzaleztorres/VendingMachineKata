﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineBusinessLogic
{
    public class DimeFeatures : IUSCoinFeatures
    {
        public USCoinTypes Type()
        {
            return USCoinTypes.Dime;
        }
    }
}
