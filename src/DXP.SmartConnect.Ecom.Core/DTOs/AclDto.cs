using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class AclDto
    {

        public AclPermissionDto All { get; set; }


        public List<AclPermissionDto> Users { get; set; }


        public List<AclPermissionDto> Roles { get; set; }


        public List<AclPermissionDto> Groups { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static AclDto FromJson(string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<AclDto>(json);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static AclDto DefaultReadACL(Guid? owner = null)
        {
            var acl = new AclDto()
            {
                All = new AclPermissionDto("All", true, false),
                Users = new List<AclPermissionDto>(),
                Roles = new List<AclPermissionDto>(),
                Groups = new List<AclPermissionDto>()
            };

            if (owner.HasValue)
            {
                acl.Users.Add(new AclPermissionDto(owner.Value.ToString(), true, true));
            }
            return acl;
        }

        public static AclDto DefaultWriteACL()
        {
            var acl = new AclDto()
            {
                All = new AclPermissionDto("All", true, true),
                Users = new List<AclPermissionDto>(),
                Roles = new List<AclPermissionDto>(),
                Groups = new List<AclPermissionDto>()
            };

            return acl;
        }

        public static AclDto DefaultOwnerACL(string owner)
        {
            var acl = new AclDto()
            {
                All = new AclPermissionDto("All", false, false),
                Users = new List<AclPermissionDto>(),
                Roles = new List<AclPermissionDto>(),
                Groups = new List<AclPermissionDto>()
            };

            Guid id;
            if (Guid.TryParse(owner, out id))
            {
                acl.Users.Add(new AclPermissionDto(id.ToString(), true, true, true));
            }
            else
            {
                acl.All = new AclPermissionDto("All", true, false);
            }

            return acl;
        }
    }
}
