using Newtonsoft.Json;

namespace Automation.Core.Helpers
{
    public class JsonHelper
    {
        public static T ReadJsonFromString<T>(string jsonData)
        {
            try
            {
                T result = JsonConvert.DeserializeObject<T>(jsonData);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                throw;
            }
        }
    }
}
