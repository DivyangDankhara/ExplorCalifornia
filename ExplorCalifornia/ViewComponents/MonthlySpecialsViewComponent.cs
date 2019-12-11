using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExplorCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExplorCalifornia.ViewComponents
{
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        public readonly BlogDataContext _db;
        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var specials = _db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
