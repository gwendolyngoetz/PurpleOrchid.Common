using Newtonsoft.Json;
using PurpleOrchid.Common.Serialization;

namespace PurpleOrchid.Common.Model
{
    public abstract class BaseObject
    {
        protected string ToJson(object obj, Formatting serializationFormatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, serializationFormatting, CustomJsonSerializerSettings.Instance.Settings);
        }

        public override string ToString()
        {
            return ToJson(this);
        }
    }
}
