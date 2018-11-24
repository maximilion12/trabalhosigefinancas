using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.POCO
{
    public class EventoPoco
    {
        public int id { get; set; }
        public int idEvento { get; set; }
        public decimal orcamento { get; set; }
        public decimal gasto { get; set; }
        public string fornecedor { get; set; }
        public int idSetor { get; set; }
        public Nullable<System.DateTime> data { get; set; }


        public static explicit operator EventoPoco(Evento solicitacao)
        {
            return new EventoPoco
            {
                id = solicitacao.id,
                idEvento = solicitacao.idEvento,
                orcamento = solicitacao.orcamento,
                gasto = solicitacao.gasto,
                fornecedor = solicitacao.fornecedor,
                idSetor = solicitacao.idSetor,
                data = solicitacao.data
            };
        }
        
        public static explicit operator Evento(EventoPoco solicitacao)
        {
            return new Evento
            {
                id = solicitacao.id,
                idEvento = solicitacao.idEvento,
                orcamento = solicitacao.orcamento,
                gasto = solicitacao.gasto,
                fornecedor = solicitacao.fornecedor,
                idSetor = solicitacao.idSetor,
                data = solicitacao.data
            };
        }
        
    }
}