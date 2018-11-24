using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models
{
    public class SolicitacaoCompraPoco
    {
        public int id { get; set; }
        public int idSetor { get; set; }
        public string descricao { get; set; }
        public System.DateTime dataPedido { get; set; }
        public System.DateTime dataVencimento { get; set; }
        public bool aprovada { get; set; }
        public Nullable<int> idContaPagar { get; set; }

        public static explicit operator SolicitacaoCompraPoco(Solicitacao_de_Compra solicitacao)
        {
            return new SolicitacaoCompraPoco
            {
                id = solicitacao.id,
                idSetor = solicitacao.idSetor,
                descricao = solicitacao.descricao,
                dataPedido = solicitacao.dataPedido,
                dataVencimento = solicitacao.dataVencimento,
                aprovada = solicitacao.aprovada,
                idContaPagar = solicitacao.idContaPagar
            };
        }


        public static implicit operator Solicitacao_de_Compra(SolicitacaoCompraPoco solicitacao)
        {
            return new Solicitacao_de_Compra
            {
                id = solicitacao.id,
                idSetor = solicitacao.idSetor,
                descricao = solicitacao.descricao,
                dataPedido = solicitacao.dataPedido,
                dataVencimento = solicitacao.dataVencimento,
                aprovada = solicitacao.aprovada,
                idContaPagar = solicitacao.idContaPagar
            };
        }
    }
}