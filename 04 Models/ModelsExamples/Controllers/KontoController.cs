using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelsExamples.Models;

namespace ModelsExamples.Controllers
{
    public class KontoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Funktionen(string id)
        {
            return Tree("Funktionen", 
                Data.Funktionen, id);
        }

        public IActionResult SGBilanz(string id)
        {
            return Tree("Sachgruppen Bilanz", 
                Data.SachgruppenBilanz, id);
        }

        public IActionResult SGER(string id)
        {
            return Tree("Sachgruppen Erfolgsrechnung", 
                Data.SachgruppenER, id);
        }

        public IActionResult SGIR(string id)
        {
            return Tree("Sachgruppen Investitionsrechnung", 
                Data.SachgruppenIR, id);
        }

        public IActionResult ER(string funktion, string sachgruppe)
        {
            ViewData["Type"] = "ER";

            return View("ER_IR", new ER_IR_ViewModel
            {
                Title = "Erfolgsrechnung",
                Funktionen = Data.Funktionen,
                Sachgruppen = Data.SachgruppenER,
                Konten = Data.ER,
                Konto = new Konto
                {
                    Funktion = Data.Funktionen.Find(funktion), 
                    Sachgruppe = Data.SachgruppenER.Find(sachgruppe)
                }
            });
        }

        public IActionResult IR(string funktion, string sachgruppe)
        {
            ViewData["Type"] = "IR";

            return View("ER_IR", new ER_IR_ViewModel
            {
                Title = "Investitionsrechnung",
                Funktionen = Data.Funktionen,
                Sachgruppen = Data.SachgruppenIR,
                Konten = Data.IR,
                Konto = new Konto
                {
                    Funktion = Data.Funktionen.Find(funktion),
                    Sachgruppe = Data.SachgruppenIR.Find(sachgruppe)
                }
            });
        }

        public IActionResult Create(string funktion, string sachgruppe, string type)
        {
            ViewData["Type"] = type;

            return View(new Konto 
            {
                Nummer = 0,
                Name = "",
                Funktion = Data.Funktionen.Find(funktion),
                Sachgruppe = ((type == "ER") ? Data.SachgruppenER : Data.SachgruppenIR).Find(sachgruppe)
            });
        }

        [HttpPost]
        public IActionResult Create(Konto konto, string funktion, string sachgruppe, string type)
        {
            ViewData["Type"] = type;

            konto.Funktion = Data.Funktionen.Find(funktion);
            konto.Sachgruppe = ((type == "ER") ? Data.SachgruppenER : Data.SachgruppenIR).Find(sachgruppe);

            if (ModelState.IsValid)
            {
                try
                {
                    var list = ((type == "ER") ? Data.ER : Data.IR);
                    list.Add(konto.Kontonummer, konto);

                    return RedirectToAction(type);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }

            return View(konto);
        }

        protected IActionResult Tree(string title, KontoGruppenListe list, string id)
        {
            ViewData["Id"] = id;
            ViewData["Title"] = title;
            return View("Tree", list);
        }
    }
}