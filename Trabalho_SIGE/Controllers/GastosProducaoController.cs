using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_SIGE.Models;
using Trabalho_SIGE.Models.POCO;

namespace Trabalho_SIGE.Controllers
{
    public class GastosProducaoController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        [ResponseType(typeof(List<ContasPagarPorSetor>))]
        public IHttpActionResult GetGastosProducao()
        {
            List<PedidosPoco> saida = new List<PedidosPoco>();

            db.Pedidos.ToList().ForEach(p => saida.Add((PedidosPoco)p));
            return Json(saida);
        }

        [ResponseType(typeof(List<ContasPagarPorSetor>))]
        public IHttpActionResult GetGastosProducao(int mes)
        {
            List<PedidosPoco> saida = new List<PedidosPoco>();

            db.Pedidos.Where(p=> p.date.ToString("MM") == mes.ToString("MM")).ToList().ForEach(p => saida.Add((PedidosPoco)p));
            return Json(saida);
        }


    }
}
