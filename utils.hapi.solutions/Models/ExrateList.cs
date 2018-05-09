using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace utils.hapi.solutions.Models
{

    public class Exrate
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Buy { get; set; }
        public string Transfer { get; set; }
        public string Sell { get; set; }
    }

    public class ExrateList
    {
        public string DateTime { get; set; }
        public List<Exrate> Exrate { get; set; }
        public string Source { get; set; }
    }

    public class RootObject
    {
        public ExrateList ExrateList { get; set; }
    }
}