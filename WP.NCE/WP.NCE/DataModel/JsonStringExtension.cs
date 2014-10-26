using Windows.Data.Json;

namespace WP.NCE.DataModel
{
    public static class JsonStringExtension
    {
        public static string GetJsonString(this IJsonValue obj)
        {
            if (obj == null || obj.ValueType == JsonValueType.Null)
            {
                return string.Empty;
            }

            return obj.GetString();
        }
    }
}