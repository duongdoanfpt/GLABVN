using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASMFULL_NET104.Extensions
{
    public static class SessionsHelper
    {
        public static void SetObjAsJson(this ISession sess, string key, object value)
        {
            sess.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjFromJson<T> (this ISession sess, string key)
        {
            var value = sess.GetString(key);
            return value is null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
