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

            //Init MongoWriterGrain and set collectionName
            //var mongoWriterGrain = GrainClient.GrainFactory.GetGrain<IMongoWriterGrain>("test");
            //mongoWriterGrain.SetCollection("test1");

            // Init (or Load) one publisher
            var publisherCrawlerGrain = GrainClient.GrainFactory.GetGrain<IPublisherGrain>(uri);
            publisherCrawlerGrain.SetArticlePageXPath("");
            publisherCrawlerGrain.SetCategoryPageXPath("");

            // Add a uri to kickstart the process
            var azureQueueGrain = GrainClient.GrainFactory.GetGrain<IAzureQueueGrain>(0);
            azureQueueGrain.Insert(uri);
            
            // Wait till the end of time
            Console.ReadKey();
        }
    }
}
