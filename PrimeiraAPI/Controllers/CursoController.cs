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
        public static List<Curso> Cursos = new List<Curso>();

        [HttpGet("")]
        public JsonResult Listar()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados"))
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
                using (var banco = new LiteDatabase(@"..\BancoDados"))
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
        public object Editar([FromBody] Curso a)
        {
            try
            {
                var Curso = Cursos.First(x => x.Codigo == a.Codigo);
                var indice = Cursos.IndexOf(Curso);
                Cursos[indice] = a;
                return a;
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 422;
                return new { MensagemErro = ex.Message };
            }
        }

        [HttpDelete("{codigo}")]
        public Curso Deletar(int codigo)
        {
            var Curso = Cursos.First(x => x.Codigo == codigo);
            Cursos.Remove(Curso);
            return Curso;
        }
    }
}
