using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.POCO
{
    public class ProducaoPoco
    {
        public string mes { get; set; }
        public int idProduto { get; set; }
        public string turno { get; set; }
        public double valor { get; set; }

        

        public static explicit operator ProducaoPoco(Producao solicitacao)
        {
            return new ProducaoPoco
            {
                mes = solicitacao.mes,
                idProduto = solicitacao.idProduto,
                turno = solicitacao.turno,
                valor = (double) (solicitacao.quantidade * solicitacao.Produto.preco)
            };
        }

        public static explicit operator Producao(ProducaoPoco solicitacao)
        {
            return new Producao
            {
                mes = solicitacao.mes,
                idProduto = solicitacao.idProduto,
                turno = solicitacao.turno
            };
        }


    }
}