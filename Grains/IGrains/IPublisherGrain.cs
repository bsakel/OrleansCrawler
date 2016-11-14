using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;

namespace Grains.IGrains
{
    public interface IPublisherGrain : IGrainWithStringKey
    {
        Task SetCategoryPageXPath(string xpath);
        Task<string> GetCategoryPageXPath();

        Task SetArticlePageXPath(string xpath);
        Task<string> GetArticlePageXPath();
    }

    public class PublisherState
    {
        public string CategoryXPath { get; set; }
        public string ArticleXPath { get; set; }
    }

    [StorageProvider(ProviderName = "AzureStore")]
    public class PublisherGrain : Grain<PublisherState>, IPublisherGrain
    {
        public Task SetCategoryPageXPath(string xpath)
        {
            State.ArticleXPath = xpath;

            return WriteStateAsync();
        }

        public Task<string> GetCategoryPageXPath()
        {
            return Task.FromResult(State.CategoryXPath);
        }

        public Task SetArticlePageXPath(string xpath)
        {
            State.CategoryXPath = xpath;

            return WriteStateAsync();
        }

        public Task<string> GetArticlePageXPath()
        {
            return Task.FromResult(State.ArticleXPath);
        }
    }
}
