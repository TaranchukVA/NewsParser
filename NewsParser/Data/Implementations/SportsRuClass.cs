using HtmlAgilityPack;
using NewsParser.Data.Tables;
using NewsParser.Data.Types;
using System;
using System.Linq;

namespace NewsParser.Data.Implementations
{
    public class SportsRuClass : NewsClass
    {
        const string uri = @"https://www.sports.ru/news/?page=1";

        public SportsRuClass(NewsContext context) : base(context)
        { }

        public override void AddNews()
        {

            var web = new HtmlWeb();
            var page = web.Load(uri);


            var list = page.DocumentNode.SelectNodes("//a[@class='short-text']").Take(100);

            foreach (var elem in list)
            {
                var href = elem.GetAttributeValue("href", null);

                var res = ParseSportsRu(href);

                AddElem(res);

            }

        }

        private NewsStorage ParseSportsRu(string href)
        {
            if (String.IsNullOrEmpty(href)) return null;
            if (!href.StartsWith("/")) return null;

            string link = $"https://www.sports.ru{href}";
            string pageMain = "//article[@class='news-item js-active']";
            string pathTitle = "//h1[@class='h1_size_tiny']";
            string pathText = "//div[@class='news-item__content js-mediator-article']";
            string pathDate = "//time[@class='time-block time-block_bottom time-block_lh30']";
            string dateElem = "datetime";

            var web = new HtmlWeb();
            var news = web.Load(link).DocumentNode.SelectSingleNode(pageMain);

            var title = news.SelectSingleNode(pathTitle).InnerText.Trim();
            var text = news.SelectSingleNode(pathText).InnerText.Trim();
            var date = news.SelectSingleNode(pathDate).GetAttributeValue(dateElem, null);

            if (String.IsNullOrEmpty(title)) return null;
            if (String.IsNullOrEmpty(text)) return null;
            if (String.IsNullOrEmpty(date)) return null;


            return new NewsStorage()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Text = text,
                Link = link,
                Published = DateTime.Parse(date),
                Source = NewsSource.SportsRu
            };
        }
    }
}
