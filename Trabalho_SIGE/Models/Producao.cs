//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trabalho_SIGE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producao
    {
        public int id { get; set; }
        public string mes { get; set; }
        public int idProduto { get; set; }
        public string turno { get; set; }
        public int quantidade { get; set; }
    
        public virtual Produto Produto { get; set; }
    }
}
