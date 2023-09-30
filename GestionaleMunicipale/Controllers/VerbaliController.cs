using Municipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipale.Controllers
{
    public class VerbaliController : Controller
    {
        
        public ActionResult Index()
        {
            List<Verbale> verbali = Db.GetVerbale();
            return View(verbali);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            List<Anagrafica> anagrafiche = Db.getAnagrafiche();
            List<SelectListItem> dropAnagrafiche = anagrafiche.Select(e => new SelectListItem
            {
                Value = e.IDAnagrafica.ToString(),
                Text = $"{e.Nome} {e.Cognome}"
            }).ToList();
            ViewBag.Anagrafiche = dropAnagrafiche;

            List<TipoViolazione> violazioni = Db.GetViolazioni();
            List<SelectListItem> dropViolazioni = violazioni.Select(e => new SelectListItem
            {
                Value = e.IdViolazione.ToString(),
                Text = e.Descrizione.ToString()
            }).ToList();
            ViewBag.Violazioni = dropViolazioni;




            return View();
        }

        [HttpPost]
        public ActionResult Create(Verbale verbale) 
        { 
            
            Db.addVerbale(verbale);
            return RedirectToAction("Index","Verbali");
        }


        [HttpGet]
        public ActionResult filterImporto () 
        {
            List<Verbale> verbali = Db.GetVerbaleImporto();
        
        return View(verbali);
        
        
        }


        [HttpGet]
        public ActionResult filterAnagrafica()
        {
            List<Anagrafica> Anagrafiche = Db.GetVerbaleAnagrafica();

            return View(Anagrafiche);


        }


        [HttpGet]
        public ActionResult filterPuntiAnagrafica()
        {
            List<Anagrafica> Anagrafiche = Db.GetPuntiAnagrafica();

            return View(Anagrafiche);
        }

        [HttpGet]
        public ActionResult filterVerbaliPunti()
        {
            List<Verbale> verbali = Db.GetVerbaliPunti10();

            return View(verbali);


        }

    }
}