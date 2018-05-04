using PrimeiraAPI.Models;
using PrimeiraAPI.Services;

namespace PrimeiraAPI.Controllers
{
    public class ProfessorController : ApiController<Professor>
    {
        public ProfessorController(CrudService<Professor> servico) : base(servico)
        {
        }
    }
}