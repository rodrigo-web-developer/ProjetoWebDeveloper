using LiteDB;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventoController : ApiController<Evento>
    {
        public static List<Evento> Eventos = new List<Evento>();
        [HttpGet("")]
        public JsonResult Listar()
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var eventos = banco.GetCollection<Evento>()
                    .Include(e => e.Participantes)
                    .FindAll().ToList();
                return new JsonResult(eventos);
            }
        }

        [HttpPut("{codigo}/[action]")]
        public JsonResult AdicionarParticipante(int codigo, [FromBody] Aluno a)
        {
            using (var banco = new LiteDatabase(@"..\BancoDados.db"))
            {
                var colecao = banco.GetCollection<Evento>();
                var evento = colecao.FindById(codigo);
                evento.Participantes.Add(a);
                colecao.Update(evento);
                return new JsonResult(evento);
            }
        }
    }
}
