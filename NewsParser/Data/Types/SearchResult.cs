using System;

namespace NewsParser.Data.Types
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public DateTime Published { get; set; }
        public NewsSource Source { get; set; }
    }
}
