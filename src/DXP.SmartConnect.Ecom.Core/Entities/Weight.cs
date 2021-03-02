using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class Weight
    {
        public Weight()
        {
            Range = new List<int>();
        }

        public string Abbreviation { set; get; }
        public string Type { set; get; }
        public string Label { set; get; }
        public int? Size { set; get; }
        public  int? MaxSize { set; get; }
        public int? MinSize { set; get; }
        public List<int> Range { set; get; }
    }
}
