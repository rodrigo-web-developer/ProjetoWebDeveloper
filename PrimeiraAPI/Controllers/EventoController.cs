using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventoController : Controller
    {
        public static List<Evento> Eventos = new List<Evento>();

        [HttpGet("")]
        public List<Evento> Listar()
        {
            return Eventos;
        }
        [HttpPost("")]
        public JsonResult Criar([FromBody] Evento a)
        {
            if (ModelState.IsValid)
            {
                Eventos.Add(a);
                return new JsonResult(a);
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }

        [HttpPut("")]
        public object Editar([FromBody] Evento a)
        {
            try
            {
                var evento = Eventos.First(x => x.Codigo == a.Codigo);
                var indice = Eventos.IndexOf(evento);
                Eventos[indice] = a;
                return a;
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 422;
                return new { MensagemErro = ex.Message };
            }
        }

        [HttpDelete("{codigo}")]
        public Evento Deletar(int codigo)
        {
            var evento = Eventos.First(x => x.Codigo == codigo);
            Eventos.Remove(evento);
            return evento;
        }

        [HttpPut("{codigo}/[action]")]
        public JsonResult AdicionarParticipante(int codigo, [FromBody] Aluno a)
        {
            var evento = Eventos.FirstOrDefault(e => e.Codigo == codigo);
            if(evento == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { MensagemErro = "Evento não encontrado" });
            }
            var aluno = (from al in AlunoController.Alunos where al.Ra == a.Ra select al).FirstOrDefault();

            if (aluno == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { MensagemErro = "Aluno não existe" });
            }

            evento.Participantes.Add(aluno);

            return new JsonResult(evento);
        }
    }
}
