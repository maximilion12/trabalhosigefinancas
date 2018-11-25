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
using Trabalho_SIGE.Models.Entradas;
using Trabalho_SIGE.Models.POCO;

namespace Trabalho_SIGE.Controllers
{
    public class ProdutoController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        [ResponseType(typeof(List<ProdutoPoco>))]
        public IHttpActionResult GetProduto()
        {
            List<ProdutoPoco> saida = new List<ProdutoPoco>();

            db.Produto.OrderBy(p=> p.dataCotacao).ToList().ForEach(p => saida.Add((ProdutoPoco)p));

            return Json(saida);
        }

        [ResponseType(typeof(ProdutoPoco))]
        public IHttpActionResult GetProduto(int id)
        {
            Produto produto = db.Produto.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok((ProdutoPoco)produto);
        }

        [Route("api/Produto/mes/{mes:int}")]
        [ResponseType(typeof(List<ProdutoPoco>))]
        public IHttpActionResult GetProdutoMes(int mes)
        {
            List<ProdutoPoco> saida = new List<ProdutoPoco>();

            db.Produto.Where(p=>p.dataCotacao.Month == mes).OrderBy(p => p.dataCotacao).ToList().ForEach(p => saida.Add((ProdutoPoco)p));

            return Json(saida);
        }


        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduto(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.id)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        [ResponseType(typeof(Produto))]
        public IHttpActionResult PostProduto(ProdutoEntrada produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Produto prod = (Produto)produto;

            db.Produto.Add(prod);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prod.id }, prod);
        }

        [ResponseType(typeof(Produto))]
        public IHttpActionResult DeleteProduto(int id)
        {
            Produto produto = db.Produto.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produto.Remove(produto);
            db.SaveChanges();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produto.Count(e => e.id == id) > 0;
        }
    }
}