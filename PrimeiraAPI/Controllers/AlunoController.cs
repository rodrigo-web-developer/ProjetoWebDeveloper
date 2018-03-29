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
        public Aluno Criar([FromBody] Aluno a)
        {
            Alunos.Add(a);
            return a;
        }

        [HttpPut("")]
        public object Editar([FromBody] Aluno a)
        {
            try
            {
                var aluno = Alunos.First(x => x.Ra == a.Ra);
                var indice = Alunos.IndexOf(aluno);
                Alunos[indice] = a;
                return a;
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 422;
                return new { MensagemErro = ex.Message };
            }
        }

        [HttpDelete("{ra}")]
        public Aluno Deletar(string ra)
        {
            var aluno = Alunos.First(x => x.Ra == ra);
            Alunos.Remove(aluno);
            return aluno;
        }
    }
}
