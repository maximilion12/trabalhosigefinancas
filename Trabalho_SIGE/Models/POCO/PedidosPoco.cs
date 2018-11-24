using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.POCO
{
    public class PedidosPoco
    {
        public int id { get; set; }
        public string idSetor { get; set; }        
        public int idProduto { get; set; }
        public decimal QTD { get; set; }
        public System.DateTime date { get; set; }


        public static explicit operator PedidosPoco(Pedidos solicitacao)
        {
            return new PedidosPoco
            {
                id = solicitacao.id,
                idSetor = solicitacao.Setor.nome,
                idProduto = solicitacao.idProduto,
                QTD = solicitacao.QTD,
                date = solicitacao.date
            };
        }

        public static explicit operator Pedidos(PedidosPoco solicitacao)
        {
            return new Pedidos
            {
                id = solicitacao.id,
                idProduto = solicitacao.idProduto,
                QTD = solicitacao.QTD,
                date = solicitacao.date
            };
        }


    }
}