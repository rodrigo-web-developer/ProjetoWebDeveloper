using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    public class CursoController : ApiController<Curso>
    {
        public CursoController(string connString) : base(connString)
        {
        }
    }
}
