using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services;

namespace PrimeiraAPI.Controllers
{
    public class CursoController : ApiController<Curso>
    {
        public CursoController(CrudService<Curso> servico) : base(servico)
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
