using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_SIGE.Models;



namespace Trabalho_SIGE.Controllers
{
    public class FuncoesBanco
    {
        Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        public double ObtemProvisoesSetorData(DateTime data, int setor)
        {
            double retorno = 0;

            Conta cont = db.Conta.SingleOrDefault(c => c.idSetor == setor && c.nome == "Provisionado");
            if (cont != null)
            {
                List<Transacao> trans = cont
                            .Transacao
                                .Where(t => t.data >= data)
                                    .ToList();

                if (trans.Count > 0)
                {
                    trans.ForEach(t => retorno += (double)t.valor);
                }
            }                
            return retorno;
        }
    }
}