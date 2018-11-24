using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.Entradas
{
    public class VendaEntrada
    {
        public int idVenda { get; set; }
        public double valor { get; set; }
        public Nullable<System.DateTime> data { get; set; }
    }
}