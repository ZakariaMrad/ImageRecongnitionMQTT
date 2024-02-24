using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ImageRecognitionMQTT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            // Get the IP
            string myIP = Dns.GetHostEntry(hostName)
                .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)!
                .ToString();

            CreateHostBuilder(args, myIP).Build().Run();
            Constants.BASE_URL = $"http://{myIP}:5000";
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string myIP) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls($"http://{myIP}:5000");
                });
    }
}
