﻿using System;
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

    public class ArticlePage
    {
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
    }

    public class ArticlePageGrain : Grain, IArticlePageGrain
    {
        public Task<bool> LoadPage()
        {
            var article = new ArticlePage();

            var saveMongo = GrainFactory.GetGrain<IMongoGrain>("test");
            saveMongo.SetCollection("test1");
            saveMongo.Save(article);

            return Task.FromResult(true);
        }
    }
}
