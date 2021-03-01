using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.SharedKernel.Helpers
{
    public static class CacheKeyHelper
    {
        private const string OBJECT_KEY_FORMAT = "{0}:PK:{1}";

        public static string GetObjectKey(string objName, string objId)
        {
            return string.Format(OBJECT_KEY_FORMAT, objName.ToLower(), objId.ToLower());
        }

        private const string QUERY_KEY_FORMAT = "{0}:QRY:{1}:{2}";

        public static string GetQueryKey(string setName, string queryName, params string[] parameters)
        {
            return string.Format(QUERY_KEY_FORMAT, setName.ToLower(), queryName.ToLower(), string.Join(":", parameters).ToLower());
        }


        private const string RELATION_KEY_FORMAT = "{0}:PK:{1}:REL:{2}";

        public static string GetRelationKey(string objName, string objId, string relationName)
        {
            return string.Format(RELATION_KEY_FORMAT, objName.ToLower(), objId.ToLower(), relationName);
        }
    }
}
