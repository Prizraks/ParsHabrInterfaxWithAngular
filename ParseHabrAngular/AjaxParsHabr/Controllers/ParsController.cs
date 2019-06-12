using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AjaxParsHabr.Models;
using System.Web;

namespace AjaxParsHabr.Controllers
{
    [ApiController]
    public class ParsController : Controller
    {
        private readonly NewsContext db;
        public ParsController(NewsContext context)
        {
            db = context;
        }
        // public  List<NewsModel> AddList(IEnumerable<object> data){
        //     List<NewsModel> listNews=new List<NewsModel>();
        //     data.ForEach(x =>
        //             {
        //                 NewsModel Obj = new NewsModel();
        //                 Obj.Name = x.Name;
        //                 Obj.Descr = x.Descr;
        //                 Obj.SourceName = x.SourceName;
        //                 Obj.DatePubl = x.DatePubl;
        //                 Obj.TimePubl=x.TimePubl;
        //                 listNews.Add(Obj);
        //             });
        //     return(listNews);
        // }

        [HttpGet]
        [Route("api/news/get")] //return full listNews
         public IActionResult GetAllNews( string idUser)
        {
            List<NewsModel> listNews = new List<NewsModel>();
            var news = db.News.Join(db.Sources,
               p => p.SourceId,
               v => v.Id,
               (p, v) => new
               {
                   Name = p.Headline,
                   Descr = p.Description,
                   SourceName = v.Name,
                   DatePubl = p.PublicationDate.ToShortDateString(),
                   TimePubl=p.PublicationDate.ToShortTimeString()
               }).ToList();
               news.ForEach(x =>
                    {
                        NewsModel Obj = new NewsModel();
                        Obj.Name = x.Name;
                        Obj.Descr = x.Descr;
                        Obj.SourceName = x.SourceName;
                        Obj.DatePubl = x.DatePubl;
                        Obj.TimePubl=x.TimePubl;
                        listNews.Add(Obj);
                    });
                return Ok(listNews);
            } 
            [HttpPost]
            [Route ("api/filterSort/get")] //filter and sort    
            public IActionResult  GetFilterSort([FromBody] FilterSortModel data){
                List<NewsModel> listNews = new List<NewsModel>();
                var news = db.News.Join(db.Sources,
                p => p.SourceId,
                v => v.Id,
                (p, v) => new
                {
                    Name = p.Headline,
                    Descr = p.Description,
                    SourceName = v.Name,
                    DatePubl = p.PublicationDate

                });
                int count = news.Count();

                if (data.sourceName!="all")// sort TRUE
                {
                    var listSort = news.Where(v => v.SourceName == data.sourceName);
                    if (data.sort == "date") // sort by date
                    {
                        var sort =listSort.OrderBy(p => p.DatePubl).ToList();
                        sort.ForEach(x =>
                        {
                            NewsModel Obj = new NewsModel();
                            Obj.Name = x.Name;
                            Obj.Descr = x.Descr;
                            Obj.SourceName = x.SourceName;
                            Obj.DatePubl = x.DatePubl.ToShortDateString();
                            Obj.TimePubl=x.DatePubl.ToShortTimeString();
                            listNews.Add(Obj);
                        });
                        return Ok(listNews);
                    }
                    else //sort by source
                    {
                        var sort =listSort.OrderBy(p => p.SourceName).ToList();
                        sort.ForEach(x =>
                        {
                            NewsModel Obj = new NewsModel();
                            Obj.Name = x.Name;
                            Obj.Descr = x.Descr;
                            Obj.SourceName = x.SourceName;
                            Obj.DatePubl = x.DatePubl.ToShortDateString();
                            Obj.TimePubl=x.DatePubl.ToShortTimeString();
                            listNews.Add(Obj);
                        });
                        return Ok(listNews);
                    }
                }
                else //filter and sort FALSE
                {
                if (data.sort == "date") // sort by date
                    {
                        var sort =news.OrderBy(p => p.DatePubl).ToList();
                        sort.ForEach(x =>
                        {
                            NewsModel Obj = new NewsModel();
                            Obj.Name = x.Name;
                            Obj.Descr = x.Descr;
                            Obj.SourceName = x.SourceName;
                            Obj.DatePubl = x.DatePubl.ToShortDateString();
                            Obj.TimePubl=x.DatePubl.ToShortTimeString();
                            listNews.Add(Obj);
                        });
                        return Ok(listNews);
                    }
                    else //sort by source
                    {
                        var sort =news.OrderBy(p => p.SourceName).ToList();
                        sort.ForEach(x =>
                        {
                            NewsModel Obj = new NewsModel();
                            Obj.Name = x.Name;
                            Obj.Descr = x.Descr;
                            Obj.SourceName = x.SourceName;
                            Obj.DatePubl = x.DatePubl.ToShortDateString();
                            Obj.TimePubl=x.DatePubl.ToShortTimeString();
                            listNews.Add(Obj);
                        });
                        return Ok(listNews);
                    }
                }       
        }                
    }
}