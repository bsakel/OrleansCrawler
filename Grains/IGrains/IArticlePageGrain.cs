using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;

namespace Grains.IGrains
{
    public interface IArticlePageGrain : IGrainWithStringKey
    {
        Task<bool> LoadPage();
    }

    public class ArticlePageGrainState
    {
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
        public bool IsParsed { get; set; }
    }

    [StorageProvider(ProviderName = "SQLStorage")]
    public class Page : Grain<ArticlePageGrainState>, IArticlePageGrain
    {
        public Task<bool> LoadPage()
        {
            if (State.IsParsed)
            {
                State.Uri = this.GetPrimaryKeyString();
            }

            return Task.FromResult(true);
        }
    }
}
