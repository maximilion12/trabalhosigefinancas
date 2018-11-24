using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.Entradas
{
    public class ProdutoEntrada
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public System.DateTime dataCotacao { get; set; }
        
    }
}