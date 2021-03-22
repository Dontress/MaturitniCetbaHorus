using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Models
{
    public class KnihaModel
    {

        public int Id { get; set; }
        public String Nazev { get; set; }
        public String Popis { get; set; }
        public int AutorId { get; set; }
        public int DruhId { get; set; }
        public int ObdobiId { get; set; }
        public String AutorJmeno { get; set; }

        public List<ObdobiList> obdobiList { get; set; }
        public List<AutoriList> autoriList { get; set; }
        public List<DruhyList> druhyList { get; set; }

        public List<int> KnihyId { get; set; }

    }


    public class ObdobiList
    {
        public int Id { get; set; }

        public string Nazev { get; set; }
    }

    public class AutoriList
    {
        public int Id { get; set; }

        public string Nazev { get; set; }
    }

    public class DruhyList
    {
        public int Id { get; set; }

        public string Nazev { get; set; }
    }



}
