using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    public class ApiController<Modelo> : Controller
    {
        private string ConnectionString;
        public ApiController(string connString)
        {
            ConnectionString = connString;
        }
        public virtual JsonResult Listar()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var cursos = banco.GetCollection<Modelo>().FindAll().ToList();
                return new JsonResult(cursos);
            }
        }

        public virtual JsonResult Criar([FromBody] Modelo c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
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
        
        public virtual JsonResult Editar([FromBody] Modelo c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
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
        
        public virtual JsonResult Deletar(int id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var curso = banco.GetCollection<Modelo>().FindById(id);
                banco.GetCollection<Modelo>().Delete(id);
                return new JsonResult(curso);
            }
        }
        [HttpGet("{id:int}")]
        public JsonResult GetById(int id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var curso = banco.GetCollection<Modelo>().FindById(id);
                return new JsonResult(curso);
            }
        }
    }
}
