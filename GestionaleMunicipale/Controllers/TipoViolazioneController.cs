using Municipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Municipale.Controllers
{
    public class TipoViolazioneController : Controller
    {
        
        public ActionResult Index()
        {
            List<TipoViolazione> Violazioni = Db.GetViolazioni();

            return View(Violazioni);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TipoViolazione violazione)
        {

            Db.AddViolazione(violazione);


            return RedirectToAction("Index");
        }

    }
}