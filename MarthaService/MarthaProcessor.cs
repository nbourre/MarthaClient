﻿using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MarthaService
{
    // Singleton de MarthaProcessor qui est ThreadSafe
    public class MarthaProcessor
    {
        private static readonly MarthaProcessor instance = new MarthaProcessor();
        private HttpClient httpClient = new HttpClient();

        IConfiguration Configuration;

        static MarthaProcessor()
        {
            
        }

        private MarthaProcessor() {
            doConfig();
                       
        }

        /// GetInstance
        public static MarthaProcessor Instance => instance;

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        void doConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange : true);

            builder.AddUserSecrets<MarthaProcessor>();

            Configuration = builder.Build();

            var user = Configuration["username"];
            var pw = Configuration["pwd"];
            var userpw = Base64Encode($"{user}:{pw}");

            httpClient.BaseAddress = new Uri(Configuration["baseAddress"]);
            httpClient.DefaultRequestHeaders.Add("auth", userpw);
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="queryName">Nom du Query sur Martha</param>
        /// <param name="param">Format JSON {"nomParam" : "valeurParam" [, ...]}</param>
        /// <returns>Une MarthaResponse</returns>
        /// <exception cref="Exception"></exception>
        public async Task<MarthaResponse> ExecuteQuery(string queryName, string param = "{}")
        {
            var url = $"queries/{queryName}/execute";
            var httpContent = new StringContent(param);

           
            using (var response = await httpClient.PostAsJsonAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<MarthaResponse>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

                    return result;

                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }


        }
    }
}