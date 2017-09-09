using Newtonsoft.Json;
using System.Data;

namespace DatatableSerializationSample
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class DatatableJsonSerializer
    {
        /// <summary>
        /// Convert datatable to json
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable datatable) {
            return JsonConvert.SerializeObject(datatable);
        }

        /// <summary>
        /// Convert json to datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDatatable(this string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
