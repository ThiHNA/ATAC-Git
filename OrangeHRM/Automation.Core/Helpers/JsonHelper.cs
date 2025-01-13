using Newtonsoft.Json;

namespace Automation.Core.Helpers
{
    public class JsonHelper
    {
        //public static T ReadJsonFromString<T>(string jsonData)
        //{
        //    try
        //    {
        //        T result = JsonConvert.DeserializeObject<T>(jsonData);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
        //        throw;
        //    }
        //}

        public static T ReadJsonFile<T>(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
