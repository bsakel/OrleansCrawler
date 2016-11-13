using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Grains.IGrains
{
    public interface IPublisherCrawler : IGrainWithStringKey
    {
        Task Crawl(string categoryPageXPath, string articlePageXPath);
        Task Add(IList<string> uriList);
    }
}
