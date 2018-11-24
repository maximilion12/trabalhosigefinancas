using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.Entradas
{
    public class ProvisionamentoCaixaEntrada
    {
        public int? idSetor { get; set; }
        public double? valor { get; set; }
        public DateTime data { get; set; }
    }
}