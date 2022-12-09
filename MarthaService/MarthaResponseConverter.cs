using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarthaService
{
    public static class MarthaResponseConverter<T>
    {
        public static List<T> Convert(MarthaResponse response)
        {
            var result = new List<T>();

            foreach (object o in response.Data)
            {
                var stringContent = o.ToString();

                var tempObject = JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                result.Add(tempObject);
            }

            return result;
        }
    }
}
