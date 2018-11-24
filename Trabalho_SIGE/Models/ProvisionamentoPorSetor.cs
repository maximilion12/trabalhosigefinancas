using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class ProvisionamentoPorSetor
    {
        public string setor { get; set; }
        public List<TransacaoPoco> provisoes { get; set; }

    }
}