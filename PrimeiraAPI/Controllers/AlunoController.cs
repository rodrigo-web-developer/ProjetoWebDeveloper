using LiteDB;
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
        public JsonResult Listar()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var cursos = banco.GetCollection<Aluno>().FindAll().ToList();
                return new JsonResult(cursos);
            }
        }
        [HttpPost("")]
        public JsonResult Criar([FromBody] Aluno c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Aluno>().Insert(c);
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
        public JsonResult Editar([FromBody] Aluno c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(@"..\BancoDados.db"))
                {
                    banco.GetCollection<Aluno>().Update(c);
                    return new JsonResult(c);
                }
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }

        [HttpDelete("{ra}")]
        public JsonResult Deletar(string ra)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Aluno>().FindById(ra);
                banco.GetCollection<Aluno>().Delete(ra);
                return new JsonResult(curso);
            }
        }

        [HttpGet("{ra}")]
        public JsonResult GetById(string ra)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var curso = banco.GetCollection<Aluno>().FindById(ra);
                return new JsonResult(curso);
            }
        }

        [HttpGet("media")]
        public JsonResult Media()
        {
            return new JsonResult(
             new
             {
                 TotalAlunos = Alunos.Count,
                 Media = Alunos.Average(a => a.Idade)
             }
            );
        }

        [HttpGet("relatorio")]
        public JsonResult Relatorio()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var alunos = banco.GetCollection<Aluno>().FindAll();

                var relatorio = from aluno in alunos
                                where aluno.Idade >= 20
                                orderby aluno.DataNascimento ascending
                                group aluno by aluno.TipoGraduacao.ToString() into alunosGraduacao
                                select new
                                {
                                    TipoGraduacao = alunosGraduacao.Key,
                                    Alunos = alunosGraduacao.ToList()
                                };
                var relatorioMetodo = alunos.Where(aluno => aluno.Idade >= 20)
                                        .OrderBy(aluno => aluno.DataNascimento)
                                        .GroupBy(aluno => aluno.TipoGraduacao.ToString())
                                        .Select(alunosGraduacao => new
                                        {
                                            TipoGraduacao = alunosGraduacao.Key,
                                            Alunos = alunosGraduacao.ToList()
                                        });

                return new JsonResult(relatorio);
            }
        }
    }
}
