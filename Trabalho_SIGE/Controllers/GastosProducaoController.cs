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

        [ResponseType(typeof(List<ProducaoPoco>))]
        public IHttpActionResult GetGastosProducao()
        {
            List<ProducaoPoco> saida = new List<ProducaoPoco>();

            db.Producao.ToList().ForEach(p => saida.Add((ProducaoPoco)p));

            
            return Json(saida);
        }

        [ResponseType(typeof(List<ProducaoPoco>))]
        public IHttpActionResult GetGastosProducao(int mes)
        {
            List<ProducaoPoco> saida = new List<ProducaoPoco>();

            string mesNome ="";
            switch (mes)
            {
                case 1:
                    mesNome = "Janeiro";
                    break;
                case 2:
                    mesNome = "Fevereiro";
                    break;
                case 3:
                    mesNome = "Março";
                    break;
                case 4:
                    mesNome = "Abril";
                    break;
                case 5:
                    mesNome = "Maio";
                    break;
                case 6:
                    mesNome = "Junho";
                    break;
                case 7:
                    mesNome = "Julho";
                    break;
                case 8:
                    mesNome = "Agosto";
                    break;
                case 9:
                    mesNome = "Outubro";
                    break;
                case 10:
                    mesNome = "Setembro";
                    break;
                case 11:
                    mesNome = "Novembro";
                    break;
                case 12:
                    mesNome = "Dezembro";
                    break;
            }

            db.Producao.Where(p=> p.mes == mesNome).ToList().ForEach(p => saida.Add((ProducaoPoco)p));
            return Json(saida);
        }

        //[ResponseType(typeof(List<ProducaoPoco>))]
        //public IHttpActionResult GetGastosProducao(string mes)
        //{
        //    List<ProducaoPoco> saida = new List<ProducaoPoco>();

        //    db.Producao.Where(p => p.mes == mes).ToList().ForEach(p => saida.Add((ProducaoPoco)p));
        //    return Json(saida);
        //}


    }
}
