using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrimeiraAPI.Controllers
{
    public class CustomController : Controller
    {
        public void Index()
        {
            Response.ContentType = "text/html; charset=utf-8";
            Response.WriteAsync("<h1>------ API ESTÁ EM EXECUÇÃO -----</h1>");
        }

        public void Http404NotFound()
        {
            Response.ContentType = "text/html; charset=utf-8";
            Response.WriteAsync("<h1 style=\"color: red\"> ------ ESSA PÁGINA NÃO EXISTE -----</h1>");
        }
    }
}
