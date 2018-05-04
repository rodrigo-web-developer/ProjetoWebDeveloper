using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using PrimeiraAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    public class EventoController : ApiController<Evento>
    {
        public string ConnectionString { get; set; }
        public EventoController(CrudService<Evento> servico) : base(servico)
        {
            ConnectionString = servico.ConnectionString;
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

        //[Authorize("Bearer")]
        public override JsonResult Criar([FromBody] Evento c)
        {
            var ignorar = ModelState.Keys.Where(k => k.StartsWith("Participantes"));
            foreach (var ignora in ignorar)
            {
                ModelState.Remove(ignora);
            }
            return base.Criar(c);
        }
    }
}
