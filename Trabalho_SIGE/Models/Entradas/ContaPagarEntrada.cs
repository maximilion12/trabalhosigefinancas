using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class ContaPagarEntrada
    {
        public System.DateTime vencimento { get; set; }
        public decimal? valor { get; set; }
        public int? idTipo { get; set; }
        public int? idSetor { get; set; }
    }
}