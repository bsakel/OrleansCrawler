using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Grains.IGrains
{
    public interface ICategoryPageGrain : IGrainWithStringKey
    {
        Task<bool> LoadPage();
    }

    public class CategoryPage
    {
        public string Uri { get; set; }
        public string Source { get; set; }
    }

    public class CategoryPageGrain : Grain, ICategoryPageGrain
    {
        public Task<bool> LoadPage()
        {
            var category = new CategoryPage();

            var saveMongo = GrainFactory.GetGrain<IMongoGrain>("test");
            saveMongo.SetCollection("test1");
            saveMongo.Save(category);

            return Task.FromResult(true);
        }
    }
}
