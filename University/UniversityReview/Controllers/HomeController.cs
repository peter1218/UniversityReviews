using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityReview.Models;
using PagedList;
using System.Web.UI;

namespace UniversityReview.Controllers
{
    public class HomeController : Controller
    {
        UniRatingSystem _db = new UniRatingSystem();



        public ActionResult Autocomplete(string term)
        {
            var model = _db.Universities.Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Name
                });
            return Json(model, JsonRequestBehavior.AllowGet);

        }

      [OutputCache(CacheProfile = "Long", VaryByHeader = "X-Requested-With;Accept-Language", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm=null,int page=1)
        {

           
            var model =
                          _db.Universities
                             .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                             .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                           
                             .Select(r => new UniversityListViewModel
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                                 City = r.City,
                                 
                                 CountOfReviews = r.Reviews.Count()
                             }).ToPagedList(page, 10);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_University", model);
            }

            return View(model);
        }




        public ActionResult About()
        {
            ViewBag.Message = "This university rating and reviews system is developed by Peter Liu";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "My contact details";

            return View();
        }

        protected override void Dispose(bool disposing)
        {

            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
