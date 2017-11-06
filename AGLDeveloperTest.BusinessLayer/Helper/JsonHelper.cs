using System.Web.Script.Serialization;

namespace AGLDeveloperTest.BusinessLayer.Helper
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string data)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var deserializedObject = javaScriptSerializer.Deserialize<T>(data);
            return deserializedObject;
        }
    }
}
