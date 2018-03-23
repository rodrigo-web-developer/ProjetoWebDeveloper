using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;

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
    }
}
