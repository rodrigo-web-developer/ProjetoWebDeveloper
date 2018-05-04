using LiteDB;
using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Services
{
    public class EventoService : CrudService<Evento>
    {
        public EventoService(string connString) : base(connString)
        {
        }

        public override List<Evento> List()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var eventos = banco.GetCollection<Evento>()
                    .Include(e => e.Participantes).FindAll().ToList();
                return eventos;
            }
        }
    }
}
