using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.Entradas
{
    public class EventoEntrada
    {
        public int idEvento { get; set; }
        public decimal orcamento { get; set; }
        public decimal gasto { get; set; }
        public string fornecedor { get; set; }
        public int idSetor { get; set; }
        public Nullable<System.DateTime> data { get; set; }

    }
}