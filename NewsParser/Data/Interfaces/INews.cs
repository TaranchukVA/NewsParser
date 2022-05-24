using NewsParser.Data.Tables;

namespace NewsParser.Data.Interfaces
{
    interface INews
    {
        void AddNews();

        void AddElem(NewsStorage news);
    }
}
