using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.Web.Mvc;
using Wwwtw.Domain;
using Wwwtw.Tasks;
using Wwwtw.Web.Mvc.Controllers.ViewModels;

namespace Wwwtw.Web.Mvc.Controllers
{
    public class ListController : Controller
    {

        private readonly MatchTasks matchTasks;

        public ListController(MatchTasks mt)
        {
            matchTasks = mt;
        }

        //
        // GET: /List/

        public ActionResult Index()
        {
            var vm = new ListIndexView();
            vm.CharityInfos = matchTasks.GetTop(50);
            vm.IsSubset = true;
            return View(vm);
        }

        public ActionResult ShowAll()
        {
            var vm = new ListIndexView();
            vm.CharityInfos = matchTasks.GetTop(5000);
            vm.IsSubset = false;
            return View("Index", vm);


        }

    }
}
