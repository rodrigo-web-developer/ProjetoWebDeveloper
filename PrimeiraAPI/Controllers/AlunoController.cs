using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
        public static List<Aluno> Alunos = new List<Aluno>();

        [HttpGet("")]
        public List<Aluno> Listar()
        {
            return Alunos;
        }
        [HttpPost("")]
        public JsonResult Criar([FromBody] Aluno a)
        {
            if (ModelState.IsValid)
            {
                Alunos.Add(a);
                return new JsonResult(a);
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }

        [HttpPut("")]
        public JsonResult Editar([FromBody] Aluno a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var aluno = Alunos.First(x => x.Ra == a.Ra);
                    var indice = Alunos.IndexOf(aluno);
                    Alunos[indice] = a;
                    return new JsonResult(a);
                }
                return new JsonResult(ModelState);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 422;
                return new JsonResult(new { MensagemErro = ex.Message });
            }
        }

        [HttpDelete("{ra}")]
        public Aluno Deletar(string ra)
        {
            var aluno = Alunos.First(x => x.Ra == ra);
            Alunos.Remove(aluno);
            return aluno;
        }

        [HttpGet("media")]
        public JsonResult Media()
        {
            return new JsonResult(
             new {
                 TotalAlunos = Alunos.Count,
                 Media = Alunos.Average(a => a.Idade)
             }    
            );
        }

        [HttpGet("")]
        public JsonResult FiltraPorNome(string nome)
        {
            // ...
        }
    }
}
