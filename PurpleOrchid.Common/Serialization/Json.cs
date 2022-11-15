using Newtonsoft.Json;

namespace PurpleOrchid.Common.Serialization
{
    public class Json
    {
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, CustomJsonSerializerSettings.Instance.Settings);
        }

        public static T? Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, CustomJsonSerializerSettings.Instance.Settings);
        }
    }
}