using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TheySay.Rest.Api
{
    public static class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerialize<T>(T t)
        {
            string jsonString;
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var obj = (T) ser.ReadObject(ms);
                return obj;
            }
        }
    }
}