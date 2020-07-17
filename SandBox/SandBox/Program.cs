﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SandBox
{
    class Root
    {
        public string Date { get; set; }
        public string PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public string Timestamp { get; set; }
        [JsonProperty("Valute")]
        public Dictionary<string,Valute> Valutes  { get; set; }
    }

    class Valute
    {
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }    
        public double Value { get; set; }   
        public double Previous { get; set; }
    }
    
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(@"https://www.cbr-xml-daily.ru/daily_json.js");
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(resp);

            foreach (var val in info.Valutes)
            {
                Console.WriteLine(val.Key + ":");
                Console.WriteLine(val.Value.Id);
                Console.WriteLine(val.Value.Name);
                Console.WriteLine(val.Value.Nominal);
                Console.WriteLine(val.Value.Previous);
                Console.WriteLine(val.Value.Value);
                Console.WriteLine(val.Value.CharCode);
                Console.WriteLine(val.Value.NumCode);
            }
        }
    }
}