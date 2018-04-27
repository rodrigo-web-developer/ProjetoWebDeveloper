using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    public class CursoController : ApiController<Curso>
    {
        public CursoController(string connString) : base(connString)
        {
        }
        [Authorize("Bearer", Roles = "Admin")]
        public override JsonResult Listar()
        {
            return base.Listar();
        }

        [Authorize("Bearer", Roles = "Admin")]
        public override JsonResult Criar([FromBody] Curso c)
        {
            return base.Criar(c);
        }
    }
}
