using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency_Handler_API.Models
{
    public class DailyCurrency
    {
        public DateTime Date{ get; set; }
        public string PreviousURL { get; set; }

        public ValutaCountry Valute{ get; set; }
    }
}
