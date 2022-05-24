using Microsoft.AspNetCore.Mvc;
using NewsParser.Data.Types;
using System;
using System.Linq;

namespace NewsParser.Data.Controllers
{


    public class DbController : Controller
    {


        private readonly NewsContext context;

        public DbController(NewsContext context)
        {
            this.context = context;
        }

        [HttpGet("db/sports.ru/{subline}")]
        public IActionResult SubTitleSportsRu([FromRoute]string subline)
        {

            if (String.IsNullOrEmpty(subline)) return BadRequest("Строка пустая");
            subline = subline.ToUpper();
            var result = context.News.Where(elem => elem.Source==NewsSource.SportsRu && elem.Title.ToUpper().Contains(subline)).ToList<SearchResult>();

            return Ok(result);
        }

        [HttpGet("db/championat.com/{subline}")]
        public IActionResult SubTitleChampionatCom([FromRoute] string subline)
        {

            if (String.IsNullOrEmpty(subline)) return BadRequest("Строка пустая");
            subline = subline.ToUpper();
            var result = context.News.Where(elem => elem.Source == NewsSource.ChampionatCom&& elem.Title.ToUpper().Contains(subline)).ToList<SearchResult>();

            return Ok(result);
        }


        [HttpGet("db/any/{subline}")]
        public IActionResult SubTitleAny([FromRoute] string subline)
        {
            if (String.IsNullOrEmpty(subline)) return BadRequest("Строка пустая");
            subline = subline.ToUpper();
            var result = context.News.Where(elem => elem.Title.ToUpper().Contains(subline)).ToList<SearchResult>();

            return Ok(result);
        }

        [HttpGet("db/all")]
        public IActionResult AllNews()
        {
            var result = context.News.ToList<SearchResult>();

            return Ok(result);
        }
    }
}
