using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Livraria.Interfaces.Gestores;
using CRUD_Livraria.Models;
using CRUD_NHIBERNATE.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace CRUD_NHIBERNATE.Controllers
{
    [Route("api/autor")]

    [ApiController]
    public class Autores :  ControllerBase
    {
        private readonly IAutorGestor _autorGestor;
        public Autores(IAutorGestor autorGestor)
        {
            _autorGestor = autorGestor;
        }

        // GET: api/<AutoreController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            IEnumerable<Autor> autores = await _autorGestor.GetAutor();

            return autores.ToList();
        }

        // GET api/<AutoreController>/5
        [HttpGet("{autorId}")]
        public async Task<ActionResult<Autor>> Get(int autorId)
        {
            Autor autor = await _autorGestor.GetAutor(autorId);

            return autor;
        }

        // POST api/<AutoreController>
        [HttpPost]
        public async Task<ActionResult<Autor>> Post(Autor autor)
        {
            var _autor = await _autorGestor.PostAutor(autor);

            return _autor;
        }

        // PUT api/<AutoreController>/5
        [HttpPut("{autorId}")]
        public async Task<IActionResult> Put(Autor autor, int autorId)
        {
            Autor _autor = await _autorGestor.PutAutor(autor, autorId);

            return Ok(autor);
        }

        // DELETE api/<AutoreController>/5
        [HttpDelete("{autorId}")]
        public async Task<ActionResult<int>> Delete(int autorId)
        {
            int linhasAfetadas = await _autorGestor.DeleteAutor(autorId);

            return linhasAfetadas;
        }

    }
}
