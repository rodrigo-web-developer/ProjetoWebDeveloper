using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    public class ProfessorController : ApiController<Professor>
    {
        public ProfessorController(string connString) : base(connString)
        {
        }
    }
}