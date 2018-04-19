using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class ApiController<Modelo> : Controller
    {
        [HttpGet("")]
        public virtual JsonResult Listar()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var cursos = banco.GetCollection<Modelo>().FindAll().ToList();
                return new JsonResult(cursos);
            }
        }
        [HttpPost("")]
        public virtual JsonResult Criar([FromBody] Modelo c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Modelo>().Insert(c);
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
        public virtual JsonResult Editar([FromBody] Modelo c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Modelo>().Update(c);
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
        public virtual JsonResult Deletar(int codigo)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Modelo>().FindById(codigo);
                banco.GetCollection<Modelo>().Delete(codigo);
                return new JsonResult(curso);
            }
        }

        [HttpGet("{codigo}")]
        public JsonResult GetById(int codigo)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Modelo>().FindById(codigo);
                return new JsonResult(curso);
            }
        }
    }
}
