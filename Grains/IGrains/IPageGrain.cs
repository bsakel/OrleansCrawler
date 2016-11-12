using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Grains.IGrains
{
    public interface IPageGrain : IGrainWithStringKey
    {
        Task<bool> LoadPage();
    }

    public class Page : Grain, IPageGrain
    {
        private string _uri;
        private string _title;
        private string _text;
        private string _source;

        public override Task OnActivateAsync()
        {
            _uri = this.GetPrimaryKeyString();
            _title = "";
            _text = "";
            _source = "";

            return base.OnActivateAsync();
        }

        public Task<bool> LoadPage()
        {
            Console.WriteLine("Page Grain for: " + _uri);

            var mongoWriter1 = GrainFactory.GetGrain<IMongoWriter>(0);
            mongoWriter1.SavePageData(_uri, _title, _text, _source);

            return Task.FromResult(true);
        }
    }
}
