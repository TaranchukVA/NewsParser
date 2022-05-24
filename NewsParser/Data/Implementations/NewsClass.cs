using NewsParser.Data.Interfaces;
using NewsParser.Data.Tables;
using System.Linq;

namespace NewsParser.Data.Implementations
{
    public abstract class NewsClass : INews
    {

        public NewsContext context;
        public NewsClass(NewsContext context)
        {
            this.context = context;
        }
        public abstract void AddNews();
        public void AddElem(NewsStorage news)
        {
            if (news != null && context.News.FirstOrDefault(elem => elem.Link == news.Link) == null)
            {
                context.Add(news);
                context.SaveChanges();
            }
        }
    }
}
