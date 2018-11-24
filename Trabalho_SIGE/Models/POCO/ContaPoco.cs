using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class ContaPoco
    {
        public int id { get; set; }
        public int idSetor { get; set; }
        public string nome { get; set; }

        public static explicit operator ContaPoco(Conta solicitacao)
        {
            return new ContaPoco
            {
                id = solicitacao.id,
                nome = solicitacao.nome,
                idSetor = solicitacao.idSetor
            };
        }

        public static implicit operator Conta(ContaPoco solicitacao)
        {
            return new Conta
            {
                id = solicitacao.id,
                nome = solicitacao.nome,
                idSetor = solicitacao.idSetor
            };
        }

    }
}