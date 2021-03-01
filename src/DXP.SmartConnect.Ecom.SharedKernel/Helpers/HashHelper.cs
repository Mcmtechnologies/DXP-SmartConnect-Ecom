using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace DXP.SmartConnect.Ecom.SharedKernel.Helpers
{
    public static class HashHelper
    {
        public static string ComputeHash(object source)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                byte[] objectAsBytes = ObjectToByteArray(source);
                byte[] result = md5.ComputeHash(objectAsBytes);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();
            }
            catch (ArgumentNullException ane)
            {
                return null;
            }
        }

        private static byte[] ObjectToByteArray(Object objectToSerialize)
        {
            MemoryStream fs = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, objectToSerialize);
                return fs.ToArray();
            }
            catch (SerializationException se)
            {
                return null;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
