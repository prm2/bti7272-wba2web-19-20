using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsExamples.Models
{
    public class KontoGruppe
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public KontoGruppenListe Untergruppen { get; } = new KontoGruppenListe();
    }

    public class KontoGruppenListe : List<KontoGruppe>
    {
        public KontoGruppe Find(string id)
        {
            foreach (var g in this)
            {
                if (g.Id == id)
                    return g;
                else if (id.StartsWith(g.Id))
                    return g.Untergruppen.Find(id);
            }

            return null;
        }
    }
}
