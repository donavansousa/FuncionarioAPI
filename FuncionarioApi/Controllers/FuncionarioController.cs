using FuncionarioApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FuncionarioApi.Controllers
{
    [EnableCors(origins: "*", headers: " * ",methods: "*")]
    public class FuncionarioController : ApiController
    {
        private Contexto contexto;

        public FuncionarioController() {
            contexto = new Contexto();
        }

        // Inserir Funcionario
        [ResponseType(typeof(Funcionario))]
        [HttpPost]
        public HttpResponseMessage PostFuncionario(Funcionario funcionario) {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Funcionario.Add(funcionario);
                    contexto.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, funcionario);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = funcionario.IdFuncionario }));
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Deletar Funcionario  
        [ResponseType(typeof(Funcionario))]
        public HttpResponseMessage DeleteFuncionario(int id)
        {
            Funcionario funcionario = contexto.Funcionario.Find(id);
            if (funcionario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
 
            contexto.Funcionario.Remove(funcionario);
 
            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
 
            return Request.CreateResponse(HttpStatusCode.OK, funcionario);


        }

        //Editar Funcionario
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutFuncionario(int id, Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != funcionario.IdFuncionario)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            contexto.Entry(funcionario).State = EntityState.Modified;

            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK,funcionario);

        }

        //Verifica se Funcionario Existe de Acordo com id
        private bool FuncionarioExiste(int id)
        {
            return contexto.Funcionario.Count(e => e.IdFuncionario == id) > 0;
        }

        //Retorna todos os Funcionarios Cadastrados
        public IEnumerable<Funcionario> GetFuncionarios() {
            return (from c in contexto.Funcionario orderby c.Nome select c).ToList();
        }

        //Retorna Funcionarios Cadastrados de Acordo com o filtro
        public IEnumerable<Funcionario> GetFilterFuncionarios(string filtro)
        {
            return (from c in contexto.Funcionario where c.Nome.Contains(filtro) orderby c.Nome select c).ToList();
        }

        //Retorna Funcionario de Acordo com id
        public IHttpActionResult GetFuncionario(int id)
        {
            var funcionario =contexto.Funcionario.FirstOrDefault(c => c.IdFuncionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(funcionario);
            }
        }

    }
}
