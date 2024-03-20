using Mava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Mava
{
    internal class JsonManager
    {
        public JsonManager() { }
        public static async Task WriteJsonConfigurationAsync()
        {
            using (FileStream fs = new FileStream(Mava_Prime.cfgpath, FileMode.OpenOrCreate))
            {

                await JsonSerializer.SerializeAsync(fs, Mava_Prime.Config);
            }
        }
        public static async Task<Configuration> ReadJsonConfigurationAsync()
        {
            if (File.Exists(Mava_Prime.cfgpath))
            {
                using (FileStream fs = new FileStream(Mava_Prime.cfgpath, FileMode.OpenOrCreate)) //https://www.cyberforum.ru/csharp-beginners/thread2729491.html wat
                {
                    return await JsonSerializer.DeserializeAsync<Configuration>(fs);
                }
            }
            return null;
        }
        public static async Task WriteJsonMappingAsync()
        {
            using (FileStream fs = new FileStream(Mava_Prime.mappath, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, Mava_Prime.Mapping);
            }
        }
        public static async Task<KeyboardMapping> ReadJsonMappingAsync()
        {
            if (File.Exists(Mava_Prime.mappath))
            {
                using (FileStream fs = new FileStream(Mava_Prime.mappath, FileMode.OpenOrCreate))
                {
                    return await JsonSerializer.DeserializeAsync<KeyboardMapping>(fs);
                }
            }
            return null;
        }
    }
}
