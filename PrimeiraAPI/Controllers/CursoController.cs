using LiteDB;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        [HttpGet("")]
        public JsonResult Listar()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var cursos = banco.GetCollection<Curso>().FindAll().ToList();
                return new JsonResult(cursos);
            }
        }
        [HttpPost("")]
        public JsonResult Criar([FromBody] Curso c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Curso>().Insert(c);
                    return new JsonResult(c);
                }
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }

        [HttpPut("")]
        public JsonResult Editar([FromBody] Curso c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Curso>().Update(c);
                    return new JsonResult(c);
                }
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }

        [HttpDelete("{codigo}")]
        public JsonResult Deletar(int codigo)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Curso>().FindById(codigo);
                banco.GetCollection<Curso>().Delete(codigo);
                return new JsonResult(curso);
            }
        }

        [HttpGet("{codigo}")]
        public JsonResult GetById(int codigo)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Curso>().FindById(codigo);
                return new JsonResult(curso);
            }
        }
    }
}
