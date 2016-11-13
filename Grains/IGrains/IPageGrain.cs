using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;

//Todo Make a POCO for the page element and savc the poco

namespace Grains.IGrains
{
    public interface IPageGrain : IGrainWithStringKey
    {
        Task<bool> LoadPage();
    }

    public class PageGrainState
    {
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
        public bool IsParsed { get; set; }
    }

    [StorageProvider(ProviderName = "SQLStorage")]
    public class Page : Grain<PageGrainState>, IPageGrain
    {
        public Task<bool> LoadPage()
        {
            if (State.IsParsed)
            {
                State.Uri = this.GetPrimaryKeyString();
            }


            var mongoWriter = GrainFactory.GetGrain<IMongoWriter>(0);
            mongoWriter.SavePageData(State.Uri, State.Title, State.Text, State.Source);

            return Task.FromResult(true);
        }
    }
}
