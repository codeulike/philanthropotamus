using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wwwtw.Domain;
namespace Wwwtw.Web.Mvc.Controllers.ViewModels
{
    public class ListIndexView
    {
        public bool IsSubset { get; set; }
        public IList<CharityInfo> CharityInfos { get; set; }
    }
}