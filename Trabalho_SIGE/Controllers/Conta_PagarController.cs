using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_SIGE.Models;

namespace Trabalho_SIGE.Controllers
{
    public class Conta_PagarController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        /// <summary>
        /// Recebe Uma conta a Pagar. necessário informar o valor, tipo (Tipo_conta) e a data vencimento.
        /// </summary>
        /// <param name="cpe">É uma entrada de conta a pagar.</param>
        /// <returns></returns>
        [ResponseType(typeof(Retorno))]
        public IHttpActionResult PostConta_Pagar(ContaPagarEntrada cpe)
        {
            if (cpe.idSetor != null || db.Setor.SingleOrDefault(s => s.id == cpe.idSetor) != null)
            {
                if (!String.IsNullOrWhiteSpace(cpe.vencimento.ToString()))
                {
                    if (cpe.valor != null)
                    {
                        Conta_Pagar ctp = new Conta_Pagar()
                        {
                            idTipo = (int)cpe.idTipo,
                            valor = (int)cpe.valor,
                            vencimento = cpe.vencimento,
                            idSetor = cpe.idSetor
                        };

                        db.Conta_Pagar.Add(ctp);

                        db.SaveChanges();
                        return Json(new { retorno = false, msg = "Valor Invalido" });
                    }
                    else
                    {
                        return Json(new { retorno = false, msg = "Valor invalido" });
                    }
                }
                else
                {
                    return Json(new { retorno = false, msg = "Vencimento Invalido" });
                }
            }
            else
            {
                return Json(new { retorno = false, msg = "Setor Invalido" });
            }
        }
       
        [ResponseType(typeof(List<ContasPagarPorSetor>))]
        public IHttpActionResult GetConta_Pagar()
        {
            List<ContasPagarPorSetor> saida = new List<ContasPagarPorSetor>();

            foreach (var item in db.Setor.ToList())
            {
                List<ContaPagarPoco> ret = new List<ContaPagarPoco>();

                db.Conta_Pagar.Where(cp => cp.idSetor == item.id).ToList().ForEach(cp => ret.Add((ContaPagarPoco)cp));

                saida.Add(new ContasPagarPorSetor() { setor = item.nome, ContasPagar = ret });

            }

            return Json(saida);
        }

        [ResponseType(typeof(ContaPagarPoco))]
        public IHttpActionResult GetConta_Pagar(int id)
        {
            Conta_Pagar conta_Pagar = db.Conta_Pagar.Find(id);
            if (conta_Pagar == null)
            {
                return NotFound();
            }
            return Json((ContaPagarPoco)conta_Pagar);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutConta_Pagar(int id, Conta_Pagar conta_Pagar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conta_Pagar.id)
            {
                return BadRequest();
            }

            db.Entry(conta_Pagar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Conta_PagarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Conta_Pagar))]
        public IHttpActionResult DeleteConta_Pagar(int id)
        {
            Conta_Pagar conta_Pagar = db.Conta_Pagar.Find(id);
            if (conta_Pagar == null)
            {
                return NotFound();
            }

            db.Conta_Pagar.Remove(conta_Pagar);
            db.SaveChanges();

            return Ok(conta_Pagar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Conta_PagarExists(int id)
        {
            return db.Conta_Pagar.Count(e => e.id == id) > 0;
        }
    }
}