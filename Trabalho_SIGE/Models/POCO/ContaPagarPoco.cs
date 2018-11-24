using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class ContaPagarPoco
    {
        public int id { get; set; }
        public System.DateTime vencimento { get; set; }
        public decimal valor { get; set; }
        public int idTipo { get; set; }


        public static explicit operator Conta_Pagar(ContaPagarPoco solicitacao)
        {
            return new Conta_Pagar
            {
                id = solicitacao.id,
                idTipo = solicitacao.idTipo,
                vencimento = solicitacao.vencimento,
                valor = solicitacao.valor
            };
        }


        public static explicit operator ContaPagarPoco(Conta_Pagar solicitacao)
        {
            return new ContaPagarPoco
            {
                id = solicitacao.id,
                idTipo = solicitacao.idTipo,
                vencimento = solicitacao.vencimento,
                valor = solicitacao.valor
            };
        }
               
    }
}