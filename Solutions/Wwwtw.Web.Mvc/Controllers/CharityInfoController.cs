using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wwwtw.Domain;

namespace Wwwtw.Web.Mvc.Controllers
{
    public class CharityInfoController : Controller
    {
        private readonly INHibernateRepository<CharityInfo> charityInfoRepository;

        public CharityInfoController(INHibernateRepository<CharityInfo> cir)
        {
            charityInfoRepository = cir;
        }

        //
        // GET: /CharityInfo/

        public ActionResult Index()
        {

            var cis = this.charityInfoRepository.GetAll();
            return View(cis);

        }

        [Transaction]
        [HttpGet]
        public ActionResult CreateOrUpdate(int id)
        {
            var ci = this.charityInfoRepository.Get(id);
            return View(ci);
        }

        [Transaction]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateOrUpdate(CharityInfo ci)
        {
            if (ModelState.IsValid && ci.IsValid())
            {
                this.charityInfoRepository.SaveOrUpdate(ci);
                return this.RedirectToAction("Index");
            }

            return View(ci);
        }

        [Transaction]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var ci = this.charityInfoRepository.Get(id);

            if (ci == null)
            {
                return HttpNotFound();
            }

            this.charityInfoRepository.Delete(ci);
            return this.RedirectToAction("Index");
        }

    }
}
