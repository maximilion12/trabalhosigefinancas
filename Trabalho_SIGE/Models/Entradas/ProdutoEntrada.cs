using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_SIGE.Models.Entradas
{
    public class ProdutoEntrada
    {
        public string nome { get; set; }
        public decimal preco { get; set; }
        public System.DateTime dataCotacao { get; set; }

        public static explicit operator ProdutoEntrada(Produto solicitacao)
        {
            return new ProdutoEntrada
            {
                nome = solicitacao.nome,
                preco = solicitacao.preco,
                dataCotacao = solicitacao.dataCotacao
            };
        }

        public static explicit operator Produto(ProdutoEntrada solicitacao)
        {
            return new Produto
            {
                nome = solicitacao.nome,
                preco = solicitacao.preco,
                dataCotacao = solicitacao.dataCotacao
            };
        }

    }
}