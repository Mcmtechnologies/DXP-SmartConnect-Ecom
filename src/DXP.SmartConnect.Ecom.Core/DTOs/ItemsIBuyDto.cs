﻿using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ItemsIBuyDto
    {
        public ItemsIBuyDto()
        {
            upcList = new List<string>();
        }
        public List<string> upcList { get; set; }
    }
}
