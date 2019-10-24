using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ex02b.Models
{
    public class Dept
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public static List<Dept> Departments
        {
            get
            {
                return new List<Dept>
                {
                    new Dept {Id = "AHB", Name="Architektur, Holz und Bau" },
                    new Dept {Id = "HAFL", Name="Hochschule für Agrar-, Forst- und Lebensmittelwissenschaften" },
                    new Dept {Id = "HKB", Name="Hochschule der Künste Bern" },
                    new Dept {Id = "TI", Name="Technik und Informatik" },
                    new Dept {Id = "W", Name="Wirtschaft" },
                    new Dept {Id = "G", Name="Gesundheit" },
                    new Dept {Id = "S", Name="Soziale Arbeit" },
                    new Dept {Id = "EHSM", Name="Eidg. Hoschule für Sport Magglingen" }
                };
            }
        }
    }
}
