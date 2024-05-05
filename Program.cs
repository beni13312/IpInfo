using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Ip_extractor
{

    public class Datas
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Timezone { get; set; }
        public string Maps { get; set; }


    }


    internal class Program
    {



        static async Task Main()
        {
            Console.Title = "Ip_extractor";

            while (true)
            {

                Console.Write("Enter ip address: ");
                string ip_address = Console.ReadLine();
                string url = $"https://ipinfo.io/{ip_address}/json";
                Console.WriteLine($"{url}");

                using (HttpClient client = new HttpClient())
                {
                    try
                    {

                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[+] Request Successfully Made");
                        Console.ResetColor();

                        string responseData = await response.Content.ReadAsStringAsync();
                        JObject obj = JObject.Parse(responseData);

                        Datas data = new Datas() {
                            Ip = (string)obj["ip"],
                            Hostname = (string)obj["hostname"],
                            City = (string)obj["city"],
                            Region = (string)obj["region"],
                            Country = (string)obj["country"],
                            Location = (string)obj["loc"],
                            Org = (string)obj["org"],
                            Postal = (string)obj["postal"],
                            Timezone = (string)obj["timezone"],


                        };

                        //Console.WriteLine(responseData);

                        Console.Write("\n");
                        Console.WriteLine($"\tIp address: {data.Ip}");
                        Console.WriteLine($"\tHostname: {data.Hostname}");
                        Console.WriteLine($"\tCity: {data.City}");
                        Console.WriteLine($"\tRegion: {data.Region}");
                        Console.WriteLine($"\tCountry : {data.Country}");
                        Console.WriteLine($"\tLocation: {data.Location}");
                        Console.WriteLine($"\tOrg: {data.Org}");
                        Console.WriteLine($"\tPostal Code: {data.Postal}");
                        Console.WriteLine($"\tTimezone: {data.Timezone}");
                        Console.Write("\n");
                        Console.WriteLine($"Google Maps: \x1B[94m https://www.google.com/maps/?q={data.Location} ");

                        Console.Write("\n");

                    }
                    catch (HttpRequestException ex)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nError: {ex.Message}\n");
                        Console.ResetColor();

                    }

                }

                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("Enter 'x' to exit.");
                Console.ResetColor ();

                if (Console.ReadKey().Key == ConsoleKey.X)
                {
                    Console.WriteLine("Exiting...");
                    break; // Exit the loop and terminate the program
                }

            }
        }
    }
}
