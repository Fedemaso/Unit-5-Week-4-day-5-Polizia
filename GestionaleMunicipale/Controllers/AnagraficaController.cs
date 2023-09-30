using Municipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipale.Controllers
{
    public class AnagraficaController : Controller
    {
        
        public ActionResult Index()
        {
            List<Anagrafica> anagrafiche = Db.getAnagrafiche();
            
            return View(anagrafiche);
        }

        [HttpGet]
        public ActionResult Create () 
        { 
        
        return View();
        }

        [HttpPost]
        public ActionResult Create(Anagrafica anagrafica)
        {
            if (ModelState.IsValid) 
            { 
                Db.addAnagrafica(anagrafica);
            return RedirectToAction("Index", "Anagrafica");
            }
            else 
            { 
            return View();
            
            }
        }


    }
}