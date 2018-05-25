using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Services;

namespace PrimeiraAPI.Controllers
{
    public class ApiController<Modelo> : Controller where Modelo: class
    {
        public CrudService<Modelo> Servico { get; set; }
        public ApiController(CrudService<Modelo> servico)
        {
            Servico = servico;
        }
        public virtual JsonResult Listar()
        {
            return new JsonResult(Servico.List());
        }
        /// <summary>
        /// Método do controller usado para criar um novo
        /// registro no banco de dados, recebendo o modelo
        /// no formato JSON por parâmetro
        /// </summary>
        /// <param name="c">Modelo serializado através do body no formato JSON</param>
        /// <returns>Retorna um JSON contendo o modelo cadastrado no banco</returns>
        public virtual JsonResult Criar([FromBody] Modelo c)
        {
            /*
             * Verifica se o modelo é valido
             * validando cada uma das propriedades
             * do modelo
             */
            if (ModelState.IsValid)
            {
                return new JsonResult(Servico.Criar(c));
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }
        
        public virtual JsonResult Editar([FromBody] Modelo c)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(Servico.Editar(c));
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }
        
        public virtual JsonResult Deletar(int id)
        {
            return new JsonResult(Servico.Excluir(id));
        }

        public virtual JsonResult GetById(int id)
        {
            return new JsonResult(Servico.Retorna(id));
        }
    }
}
