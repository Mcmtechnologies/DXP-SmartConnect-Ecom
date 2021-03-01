using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Settings
{
    public class InMemoryCacheSettings
    {
        public int ProductThreshHoldShouldCache { set; get; } = 15; //Mwg8MaxProductNeedCache in Old Smartconnect, should cache when return too many products, threshhold = 15
        public int CachedProductExpiryTime { set; get; } = 6; //default is 6 same as old code
    }
}
