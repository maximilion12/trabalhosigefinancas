using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class TransacaoPoco
    {
        public int id { get; set; }
        public int idTipo { get; set; }
        public System.DateTime data { get; set; }
        public decimal valor { get; set; }
        public int idConta { get; set; }

        public static explicit operator TransacaoPoco(Transacao solicitacao)
        {
            return new TransacaoPoco
            {
                id = solicitacao.id,
                idTipo = solicitacao.idTipo,
                data = solicitacao.data,
                valor = solicitacao.valor,
                idConta = solicitacao.idConta,
            };
        }
        
        public static explicit operator Transacao(TransacaoPoco solicitacao)
        {
            return new Transacao
            {
                id = solicitacao.id,
                idTipo = solicitacao.idTipo,
                data = solicitacao.data,
                valor = solicitacao.valor,
                idConta = solicitacao.idConta,
            };
        }

    }
}