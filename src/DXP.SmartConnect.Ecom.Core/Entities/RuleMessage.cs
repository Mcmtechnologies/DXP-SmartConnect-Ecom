using System.Collections.Generic;

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
