using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;

namespace SiloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ClusterConfiguration.LocalhostPrimarySilo();
            config.AddAzureBlobStorageProvider("AzureStore", "UseDevelopmentStorage=true");

            using (var silo = new Orleans.Runtime.Host.SiloHost("primary", config))
            {
                silo.InitializeOrleansSilo();

                var result = silo.StartOrleansSilo();
                if (result)
                {
                    Console.WriteLine("Silo Running");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Could not start Silo");
            }
        }
    }
}
