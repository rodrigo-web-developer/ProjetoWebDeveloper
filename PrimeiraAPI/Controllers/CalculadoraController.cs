using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class CalculadoraController : Controller
    {
        [HttpPost("somar")]
        public double Somar([FromBody] Dictionary<string, string> valores)
        {
            var numero1 = double.Parse(valores["numero1"]);
            var numero2 = double.Parse(valores["numero2"]);
            return numero1 + numero2;
        }
    }
}
