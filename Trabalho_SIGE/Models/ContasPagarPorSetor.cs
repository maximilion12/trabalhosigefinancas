using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class ContasPagarPorSetor
    {
        public string setor { get; set; }
        public List<ContaPagarPoco> ContasPagar { get; set; }

    }
}