using LoginAPI.Models;
using System;
using System.Web.Http;

namespace LoginAPI.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [Route]
        [HttpGet]
        // GET: api/Usuario
        public IHttpActionResult Get()
        {
            return Ok(new Usuario()
            {
                Id = 2,
                Nome = "Thomas",
                Confirmacao = "OK"
            });
        }

        [Route("{Id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(new Usuario()
            {
                Id = 2,
                Nome = "Thomas",
                Confirmacao = "OK"
            });
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Usuario model)
        {
            return Created(String.Empty, model);

        }
    }
}
