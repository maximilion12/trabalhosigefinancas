using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class SolicitacaoDeCompraPorSetor
    {
        public string setor { get; set; }
        public List<SolicitacaoCompraPoco> SolicitacoesDeCompra {get;set;}
    }
}