using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Trabalho_SIGE.Models;

namespace Trabalho_SIGE.Controllers
{
    public class Solicitacao_de_CompraController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();
        public FuncoesBanco func = new FuncoesBanco();

      
        [ResponseType(typeof(Retorno))]
        public IHttpActionResult PostSolicitacao_de_Compra(SolicitacaoDeCompraEntrada sdc)
        {
            if (sdc.idSetor != null || db.Setor.SingleOrDefault(s => s.id == sdc.idSetor) != null)
            {
                if (sdc.dataVencimento != null)
                {
                    if (sdc.dataPedido != null)
                    {
                        if (sdc.valor != null)
                        {
                            if (func.ObtemProvisoesSetorData((DateTime)sdc.dataPedido, (int)sdc.idSetor) >= sdc.valor)
                            {
                                Conta_Pagar contaPagar = new Conta_Pagar()
                                {
                                    vencimento = (DateTime)sdc.dataVencimento,
                                    valor = (decimal)sdc.valor,
                                    idTipo = 5,
                                    idSetor = sdc.idSetor
                                };
                                db.Conta_Pagar.Add(contaPagar);

                                Solicitacao_de_Compra solicitacao = new Solicitacao_de_Compra()
                                {
                                    idSetor = (int)sdc.idSetor,
                                    descricao = sdc.descricao,
                                    dataPedido = (DateTime)sdc.dataPedido,
                                    dataVencimento = (DateTime)sdc.dataVencimento,
                                    aprovada = true,
                                    idContaPagar = contaPagar.id,
                                    valor = (decimal)sdc.valor
                                };
                                db.Solicitacao_de_Compra.Add(solicitacao);

                                db.SaveChanges();

                                return Json(new { retorno = true, msg = "Pedido Confirmado" });
                            }
                            else
                            {
                                Solicitacao_de_Compra solicitacao = new Solicitacao_de_Compra()
                                {
                                    idSetor = (int)sdc.idSetor,
                                    descricao = sdc.descricao,
                                    dataPedido = (DateTime)sdc.dataPedido,
                                    dataVencimento = (DateTime)sdc.dataVencimento,
                                    aprovada = false,
                                    idContaPagar = null,
                                    valor = (decimal)sdc.valor
                                };
                                db.Solicitacao_de_Compra.Add(solicitacao);
                                db.SaveChanges();
                                return Json(new { retorno = false, msg = "Saldo Provisionamento Insuficiente" });
                            }
                        }
                        else
                        {
                            return Json(new { retorno = false, msg = "Valor Invalido" });
                        }
                    }
                    else
                    {
                        return Json(new { retorno = false, msg = "Data Pedido Invalida" });
                    }

                }
                else
                {
                    return Json(new { retorno = false, msg = "Data Vencimento Invalida" });
                }
            }
            else
            {
                return Json(new { retorno = false, msg = "Setor não incontrado" });
            }
        }

        [ResponseType(typeof(SolicitacaoDeCompraPorSetor))]
        public IHttpActionResult GetSolicitacao_de_Compra()
        {
            List<SolicitacaoDeCompraPorSetor> saida = new List<SolicitacaoDeCompraPorSetor>();
            List<SolicitacaoCompraPoco> retorno;
            foreach (var item in db.Setor.ToList())
            {
                retorno = new List<SolicitacaoCompraPoco>();
                db.Solicitacao_de_Compra.Where(sdc => sdc.idSetor == item.id)
                                .ToList()
                                    .ForEach(sdc => retorno.Add((SolicitacaoCompraPoco)sdc));

                saida.Add(new SolicitacaoDeCompraPorSetor() { setor = item.nome, SolicitacoesDeCompra = retorno.ToList() });
            }
            return Json(saida);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Solicitacao_de_CompraExists(int id)
        {
            return db.Solicitacao_de_Compra.Count(e => e.id == id) > 0;
        }
    }
}