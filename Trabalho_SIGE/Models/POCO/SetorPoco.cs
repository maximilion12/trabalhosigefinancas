using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class SetorPoco
    {
        public int id { get; set; }
        public string nome { get; set; }
               
        public static explicit operator SetorPoco(Setor solicitacao)
        {
            return new SetorPoco
            {
                id = solicitacao.id,
                nome = solicitacao.nome
            };
        }
        
        public static implicit operator Setor(SetorPoco solicitacao)
        {
            return new Setor
            {
                id = solicitacao.id,
                nome = solicitacao.nome
            };
        }

    }
}