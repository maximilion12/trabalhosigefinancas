using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.POCO
{
    public class ProdutoPoco
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public System.DateTime dataCotacao { get; set; }


        public static explicit operator ProdutoPoco(Produto solicitacao)
        {
            return new ProdutoPoco
            {
                id = solicitacao.id,
                nome = solicitacao.nome,
                preco = solicitacao.preco,
                dataCotacao = solicitacao.dataCotacao
            };
        }

        public static explicit operator Produto(ProdutoPoco solicitacao)
        {
            return new Produto
            {
                id = solicitacao.id,
                nome = solicitacao.nome,
                preco = solicitacao.preco,
                dataCotacao = solicitacao.dataCotacao
            };
        }

    }
}