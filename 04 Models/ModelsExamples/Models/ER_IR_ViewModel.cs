using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsExamples.Models
{
    public class ER_IR_ViewModel
    {
        public string Title { get; set; }
        public Konto Konto { get; set; }
        public Dictionary<string, Konto> Konten { get; set; }
        public KontoGruppenListe Funktionen { get; set; }
        public KontoGruppenListe Sachgruppen { get; set; }
    }
}
