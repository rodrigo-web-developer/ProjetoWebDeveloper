using LiteDB;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    public class EventoController : ApiController<Evento>
    {
        public EventoController(string connString) : base(connString)
        {
        }
        
        public override JsonResult Listar()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var eventos = banco.GetCollection<Evento>()
                    .Include(e => e.Participantes)
                    .FindAll().ToList();
                return new JsonResult(eventos);
            }
        }

        [HttpPut("api/[controller]/{codigo}/[action]")]
        public JsonResult AdicionarParticipante(int codigo, [FromBody] Aluno a)
        {
            using (var banco = new LiteDatabase(ConnectionString))
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
