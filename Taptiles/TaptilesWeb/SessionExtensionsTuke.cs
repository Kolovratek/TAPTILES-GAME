using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Microsoft.AspNetCore.Http
{
    public static class SessionExtensionsTuke
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var obj = session.GetString(key);
            var deserialized = JsonConvert.DeserializeObject<T>(obj);
            //Debug.WriteLine($"{DateTime.Now.ToString()} deser {deserialized}", "app");
            return deserialized;
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var serialized = JsonConvert.SerializeObject(value, Formatting.Indented);
            //Debug.WriteLine($"{DateTime.Now.ToString()} ser {serialized}", "app");
            session.SetString(key, serialized);
        }
    }
}
