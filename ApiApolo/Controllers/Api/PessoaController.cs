using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiApolo.Models;

namespace ApiApolo.Controllers.Api
{
    public class PessoaController : ApiController
    {
        private apolo db = new apolo();

        // GET: api/Pessoa
        public IQueryable<Pessoas> GetPessoas()
        {
            return db.Pessoas;
        }

        // GET: api/Pessoa/5
        [ResponseType(typeof(Pessoas))]
        public async Task<IHttpActionResult> GetPessoas(int id)
        {
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return NotFound();
            }

            return Ok(pessoas);
        }

        // PUT: api/Pessoa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPessoas(int id, Pessoas pessoas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoas.ID)
            {
                return BadRequest();
            }

            db.Entry(pessoas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoasExists(id))
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

        // POST: api/Pessoa
        [ResponseType(typeof(Pessoas))]
        public async Task<IHttpActionResult> PostPessoas(Pessoas pessoas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pessoas.Add(pessoas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pessoas.ID }, pessoas);
        }

        // DELETE: api/Pessoa/5
        [ResponseType(typeof(Pessoas))]
        public async Task<IHttpActionResult> DeletePessoas(int id)
        {
            Pessoas pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return NotFound();
            }

            db.Pessoas.Remove(pessoas);
            await db.SaveChangesAsync();

            return Ok(pessoas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoasExists(int id)
        {
            return db.Pessoas.Count(e => e.ID == id) > 0;
        }
    }
}