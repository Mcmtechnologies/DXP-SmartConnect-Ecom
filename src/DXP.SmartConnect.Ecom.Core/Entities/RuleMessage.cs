using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class RuleMessage
    {
        public RuleMessage()
        {
            Blocking = new List<object>();
            Information = new List<object>();
        }

        public List<object> Blocking { set; get; }
        public List<object> Information { set; get; }

    }
}
