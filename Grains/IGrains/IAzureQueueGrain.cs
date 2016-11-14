using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Orleans;
using Orleans.Concurrency;

namespace Grains.IGrains
{
    public interface IAzureQueueGrain : IGrainWithIntegerKey
    {
        Task Insert(string uri);
        Task<string> Peek();
        Task<string> Get();
    }

    [StatelessWorker(10)]
    public class AzureQueueGrain : Grain, IAzureQueueGrain
    {
        public Task Insert(string uri)
        {
            throw new NotImplementedException();
        }

        public Task<string> Peek()
        {
            throw new NotImplementedException();
        }

        public Task<string> Get()
        {
            throw new NotImplementedException();
        }
    }
}
