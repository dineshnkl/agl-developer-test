using System.Web.Script.Serialization;

namespace AGLDeveloperTest.BusinessLayer.Helper
{
    public static class JsonHelper
    {
        public static string Serialize(object data)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            string serializedString = null;
            if (data != null)
                serializedString = javaScriptSerializer.Serialize(data);
            return serializedString;
        }

        public static T Deserialize<T>(string data)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var deserializedObject = javaScriptSerializer.Deserialize<T>(data);
            return deserializedObject;
        }
    }
}
