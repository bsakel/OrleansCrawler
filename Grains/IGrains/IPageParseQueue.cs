using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Grains.IGrains
{
    public interface IPageParseQueue : IGrainWithIntegerKey
    {
        Task Add(string uri);
        Task Add(IList<string> uriList);
        Task<string> GetNext();
        Task<int> Count();
    }
}
