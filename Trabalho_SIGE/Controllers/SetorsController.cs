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
    public class SetorsController : ApiController
    {
        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        [ResponseType(typeof(List<SetorPoco>))]
        public IHttpActionResult GetSetor()
        {
            List<SetorPoco> saida = new List<SetorPoco>();

            db.Setor.ToList().ForEach(s => saida.Add((SetorPoco)s));
            
                return Json(saida);

        }

        [ResponseType(typeof(SetorPoco))]
        public IHttpActionResult GetSetor(int id)
        {
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return NotFound();
            }

            return Ok((SetorPoco)setor);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutSetor(int id, Setor setor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setor.id)
            {
                return BadRequest();
            }

            db.Entry(setor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
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

        [ResponseType(typeof(SetorPoco))]
        public IHttpActionResult PostSetor(Setor setor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Setor.Add(setor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = setor.id }, setor);
        }

        [ResponseType(typeof(SetorPoco))]
        public IHttpActionResult DeleteSetor(int id)
        {
            Setor setor = db.Setor.Find(id);
            if (setor == null)
            {
                return NotFound();
            }

            db.Setor.Remove(setor);
            db.SaveChanges();

            return Ok((SetorPoco)setor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetorExists(int id)
        {
            return db.Setor.Count(e => e.id == id) > 0;
        }
    }
}