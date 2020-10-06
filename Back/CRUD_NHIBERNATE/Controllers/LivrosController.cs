using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Livraria.Interfaces.Gestores;
using CRUD_Livraria.Models;
using CRUD_NHIBERNATE.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;

namespace CRUD_NHIBERNATE.Controllers
{
    [Route("api/livro")]
    [ApiController]

    public class Livros : ControllerBase
    {
        private readonly ILivroGestor _livroGestor;

        public Livros(ILivroGestor livroGestor)
        {          
            _livroGestor = livroGestor;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivro()
        {
            IEnumerable<Livro> livros = await _livroGestor.GetLivro();
            //var lista = JsonConvert.SerializeObject(livros.ToList());
            return Ok(livros.ToList());
        }

        // GET: api/Livros/5
        [HttpGet("{livroId}")]
        public async Task<ActionResult<Livro>> GetLivro(long livroId)
        {
            Livro livro = await _livroGestor.GetLivro(livroId);

            return livro;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{livroId}")]
        public async Task<IActionResult> PutLivro(Livro livro, long livroId)
        {
            Livro _livro = await _livroGestor.PutLivro(livro, livroId);

            return Ok(_livro);
        }

        [HttpPut]
        [Route("comprar/{livroId}")]
        public async Task<IActionResult> ComprarLivro(long livroId)
        {
            Livro livro = await _livroGestor.PutLivro(null, livroId);

            return Ok(livro);
        }

        //// POST: api/Livros
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            var _livro = await _livroGestor.PostLivro(livro);

            return _livro;
        }

        //// DELETE: api/Livros/5
        [HttpDelete("{livroId}")]
        public async Task<ActionResult<int>> DeleteLivro(long livroId)
        {
            int linhasAfetadas = await _livroGestor.DeleteLivro(livroId);

            return linhasAfetadas;
        }

    }
}
