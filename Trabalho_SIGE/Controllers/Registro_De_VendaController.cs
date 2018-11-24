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
    public class Registro_De_VendaController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        public IQueryable<Registro_De_Venda> GetRegistro_De_Venda()
        {
            return db.Registro_De_Venda;
        }

        [ResponseType(typeof(Registro_De_Venda))]
        public IHttpActionResult GetRegistro_De_Venda(int id)
        {
            Registro_De_Venda registro_De_Venda = db.Registro_De_Venda.Find(id);
            if (registro_De_Venda == null)
            {
                return NotFound();
            }

            return Ok(registro_De_Venda);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistro_De_Venda(int id, Registro_De_Venda registro_De_Venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registro_De_Venda.id)
            {
                return BadRequest();
            }

            db.Entry(registro_De_Venda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Registro_De_VendaExists(id))
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

        [ResponseType(typeof(Registro_De_Venda))]
        public IHttpActionResult PostRegistro_De_Venda(Registro_De_Venda registro_De_Venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Registro_De_Venda.Add(registro_De_Venda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registro_De_Venda.id }, registro_De_Venda);
        }

        [ResponseType(typeof(Registro_De_Venda))]
        public IHttpActionResult DeleteRegistro_De_Venda(int id)
        {
            Registro_De_Venda registro_De_Venda = db.Registro_De_Venda.Find(id);
            if (registro_De_Venda == null)
            {
                return NotFound();
            }

            db.Registro_De_Venda.Remove(registro_De_Venda);
            db.SaveChanges();

            return Ok(registro_De_Venda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Registro_De_VendaExists(int id)
        {
            return db.Registro_De_Venda.Count(e => e.id == id) > 0;
        }
    }
}