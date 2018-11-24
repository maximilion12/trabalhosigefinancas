using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class SolicitacaoDeCompraEntrada
    {
        public int? idSetor { get; set; }
        public string descricao { get; set; }
        public System.DateTime? dataPedido { get; set; }
        public System.DateTime? dataVencimento { get; set; }
        public double? valor { set; get; }
    }
}