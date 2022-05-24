using Microsoft.AspNetCore.Mvc;
using NewsParser.Data.Implementations;
using NewsParser.Data.Interfaces;

namespace NewsParser.Data.Controllers
{
    public class NewsController : Controller
    {

        private INews sportsRu;
        private INews championatCom;

        public NewsController(NewsContext context)
        {
            sportsRu = new SportsRuClass(context);
            championatCom = new ChampionatComClass(context);

        }

        [Route("/")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world!");
        }

        [Route("/add/sports.ru")]
        [HttpGet()]
        public IActionResult AddSportsRu()
        {
            sportsRu.AddNews();
            return Ok("Новости успешно добавлены в базу данных");
        }
        

        [Route("/add/championat.com")]
        [HttpGet()]
        public IActionResult AddChampionatCom()
        {
            championatCom.AddNews();
            return Ok("Новости успешно добавлены в базу данных");
        }

        [Route("/add")]
        [HttpGet()]
        public IActionResult AddAll()
        {
            sportsRu.AddNews();
            championatCom.AddNews();
            return Ok("Новости успешно добавлены в базу данных");
        }
    }
}
