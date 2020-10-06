using CRUD_Livraria.Interfaces.Gestores;
using CRUD_Livraria.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CRUD_Livraria.Gestores
{
    public class AutorGestor : IAutorGestor
    {
        private readonly ISession _session;
        public AutorGestor(ISession session)
        {
            _session = session;
        }

        public bool AutorExists(long id)
        {
            return _session.Query<Autor>().AnyAsync(e => e.AutorId == id).Result;
        }

        public async Task<int> DeleteAutor(long id)
        {
            int linhasAfetadas = 0;

            if (AutorExists(id))
            {
                Autor autor = GetAutor(id).Result;
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.DeleteAsync(autor);
                    await transaction.CommitAsync();
                    linhasAfetadas++;
                }
            }

            return linhasAfetadas;
        }

        public async Task<IEnumerable<Autor>> GetAutor()
        {
            List<Autor> autores = await _session.Query<Autor>().ToListAsync();

            return autores;
        }

        public async Task<Autor> GetAutor(long id)
        {
            Autor autor = await _session.GetAsync<Autor>(id);

            return autor;
        }

        public async Task<Autor> PostAutor(Autor autor)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.SaveAsync(autor);
                await transaction.CommitAsync();
            }

            return autor;
        }

        public async Task<Autor> PutAutor(Autor autor, long id)
        {
            Autor _autor = GetAutor(id).Result;
            if (_autor != null && autor != null)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _autor.Nome = autor.Nome;
                    await _session.SaveOrUpdateAsync(_autor);
                    await transaction.CommitAsync();
                }
            }

            return autor;
        }
    }
}
