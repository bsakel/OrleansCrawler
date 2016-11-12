using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime.Configuration;
using Grains.IGrains;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "www.oneman.gr";

            // initialize the grain client, with some retry logic
            var initialized = false;
            while (!initialized)
            {
                try
                {
                    GrainClient.Initialize(ClientConfiguration.LocalhostSilo());
                    initialized = GrainClient.IsInitialized;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }

            // get a reference to the grain from the grain factory
            var grain = GrainClient.GrainFactory.GetGrain<IPageGrain>(uri);

            // call the grain
            var result = grain.LoadPage().Result;

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
