using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wwwtw.Domain;
using Wwwtw.Tasks;

namespace Wwwtw.Web.Mvc.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly MatchTasks matchTasks;

        public HomeController(MatchTasks mt)
        {
            matchTasks = mt;
        }

        public ActionResult Index()
        {
            var pair = matchTasks.GetRandomPair();
            return View(pair);
        }

        [Transaction]
        public JsonResult Vote(int winnerId, int loserId)
        {
            matchTasks.MatchResult(winnerId, loserId);
            return Json(new { done = "ok" }, JsonRequestBehavior.AllowGet);
        }

    }
}
