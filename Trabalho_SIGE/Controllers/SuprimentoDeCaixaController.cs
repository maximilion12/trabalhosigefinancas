using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_SIGE.Models;
using Trabalho_SIGE.Models.Entradas;

namespace Trabalho_SIGE.Controllers
{
    public class SuprimentoDeCaixaController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();
        public FuncoesBanco func = new FuncoesBanco();

        [ResponseType(typeof(Retorno))]
        public IHttpActionResult PostSuprimentoDeCaixa(ProvisionamentoCaixaEntrada prov)
        {
            if (prov.idSetor != null && db.Setor.SingleOrDefault(s => s.id == (int)prov.idSetor) != null)
            {
                if (prov.valor != null)
                {
                    if (prov.data != null)
                    {
                        Transacao t = new Transacao()
                        {
                            idConta = db.Conta.SingleOrDefault(c => c.idSetor == prov.idSetor && c.nome == "Provisionado").id,
                            idTipo = 1,
                            data = prov.data,
                            valor= (decimal)prov.valor
                        };
                        db.Transacao.Add(t);
                        db.SaveChanges();
                        return Json(new Retorno() { retorno = true, msg = "Provisionamento Adicionado com Sucesso." });
                    }
                    else
                    {
                        return Json(new Retorno() { retorno = false, msg = "Data invalida." });
                    }
                }
                else
                {
                    return Json(new Retorno() { retorno = false, msg = "Valor invalido." });
                }
            }
            else
            {
                return Json(new Retorno() { retorno = false, msg = "Setor inválido." });
            }
        }

        [ResponseType(typeof(ProvisionamentoPorSetor))]
        public IHttpActionResult GetSuprimentoDeCaixa()
        {
            List<ProvisionamentoPorSetor> saida = new List<ProvisionamentoPorSetor>();

            foreach (var item in db.Setor.ToList())
            {
                List<TransacaoPoco> ret = new List<TransacaoPoco>();

                db.Transacao.Where(t => t.Conta.idSetor == item.id).ToList().ForEach(t => ret.Add((TransacaoPoco)t));

                saida.Add(new ProvisionamentoPorSetor() { setor = item.nome, provisoes = ret.ToList() });

            }
            return Json(saida);
        }
    }
}
