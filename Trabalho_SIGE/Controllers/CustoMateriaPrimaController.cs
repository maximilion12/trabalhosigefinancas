using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_SIGE.Models;

namespace Trabalho_SIGE.Controllers
{
    public class CustoMateriaPrimaController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        [ResponseType(typeof(List<ContasPagarPorSetor>))]
        public IHttpActionResult GetCustoMateriaPrima()
        {
            Produto saida = db.Produto.Single(p => p.id == 5);
            if (saida != null)
            {
                return Json(saida.preco);

            }
            else
            {
                return Json(new { retorno = true, msg = "SEM Retorno" });
            }
        }

        [ResponseType(typeof(List<ContasPagarPorSetor>))]
        public IHttpActionResult GetCustoMateriaPrima(int mes)
        {
            Produto saida = db.Produto.Single(p => p.id == 5 && p.dataCotacao.Month == mes);
            if (saida != null)
            {
                return Json(saida.preco);
            }
            else
            {
                return Json(new { retorno = true, msg = "SEM Retorno" });
            }
        }



    }
}
