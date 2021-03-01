using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class StorageAreaDto
    {
        public Nullable<int> storageAreaId { get; set; }
        public string storageAreaNum { get; set; }
        public string storageAreaName { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
