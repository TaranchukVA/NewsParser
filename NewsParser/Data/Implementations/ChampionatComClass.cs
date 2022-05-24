using HtmlAgilityPack;
using NewsParser.Data.Tables;
using NewsParser.Data.Types;
using System;
using System.Linq;

namespace NewsParser.Data.Implementations
{
    public class ChampionatComClass : NewsClass
    {
        public ChampionatComClass(NewsContext context) : base(context)
        { }


        public override void AddNews()
        {
            string uri = @"https://www.championat.com/news/1.html";
            string contentPath = "//div[@class='news-item__content']";

            var web = new HtmlWeb();
            var page = web.Load(uri);

            var list = page.DocumentNode.SelectNodes(contentPath).Take(100);

            foreach (var elem in list)
            {
                var href = elem.Element("a").GetAttributeValue("href", null);
                var res = ParseChampionatCom(href);
                AddElem(res);
            }

        }

        private NewsStorage ParseChampionatCom(string href)
        {
            if (String.IsNullOrEmpty(href)) return null;
            if (!href.StartsWith("/")) return null;

            string link = $"https://www.championat.com{href}";
            string pageMain = "//div[@class='page-main']";
            string pathTitle = "//div[@class='article-head__title']";
            string pathText = "//div[@data-type='news']";
            string pathDate = "//time[@class='article-head__date']";

            var web = new HtmlWeb();
            var news = web.Load(link).DocumentNode.SelectSingleNode(pageMain);

            var title = news.SelectSingleNode(pathTitle).InnerText.Trim();
            var text = news.SelectSingleNode(pathText).InnerText.Trim();
            var date = news.SelectSingleNode(pathDate).InnerText.Replace(" МСК", "");


            if (String.IsNullOrEmpty(title)) return null;
            if (String.IsNullOrEmpty(text)) return null;
            if (String.IsNullOrEmpty(date)) return null;


            return new NewsStorage()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Text = text.Split("\n\n")[0],
                Link = link,
                Published = DateTime.Parse(date),
                Source = NewsSource.ChampionatCom
            };
        }
    }
}
