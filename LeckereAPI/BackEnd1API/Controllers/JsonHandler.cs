using System.Text.Json;
using System.Text.Json.Serialization;
namespace BackEnd1API.Controllers
{
    public class JsonHandler<T>
    {
        public static string Serialize(T obj)
        {
            
            return JsonSerializer.Serialize(obj);
        }

        public static T Deserialize(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}