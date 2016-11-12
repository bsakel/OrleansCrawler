using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Configuration;

namespace SiloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var silo = new Orleans.Runtime.Host.SiloHost("primary", ClusterConfiguration.LocalhostPrimarySilo()))
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
