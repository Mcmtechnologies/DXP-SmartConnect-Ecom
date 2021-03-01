using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class AclPermissionDto
    {
        public string ObjectId { get; set; }
        public bool Access { get; set; }
        public bool Modify { get; set; }
        public bool Archive { get; set; }

        public AclPermissionDto()
        {
        }

        public AclPermissionDto(string objId, bool access, bool modify, bool archive = true)
        {
            ObjectId = objId;
            Access = access;
            Modify = modify;
            Archive = archive;
        }
    }
}
