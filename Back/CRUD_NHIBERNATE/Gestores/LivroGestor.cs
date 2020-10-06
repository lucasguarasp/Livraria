using CRUD_Livraria.Interfaces.Gestores;
using CRUD_Livraria.Models;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Livraria.Gestores
{
    public class LivroGestor : ILivroGestor
    {
        private readonly ISession _session;
        public LivroGestor(ISession session)
        {
            _session = session;
        }

        public async Task<int> DeleteLivro(long id)
        {
            int linhasAfetadas = 0;

            if (LivroExists(id))
            {

                Livro livro = GetLivro(id).Result;
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.DeleteAsync(livro);
                    await transaction.CommitAsync();
                    linhasAfetadas++;
                }
            }

            return linhasAfetadas;
        }

        public async Task<IEnumerable<Livro>> GetLivro()
        {
            List<Livro> livros = await _session.Query<Livro>().ToListAsync();

            return livros;
        }

        public async Task<Livro> GetLivro(long id)
        {
            var livro = await _session.GetAsync<Livro>(id);

            return livro;
        }

        public bool LivroExists(long id)
        {
            var livro = _session.Query<Livro>().AnyAsync(e => e.LivroId == id).Result;

            return livro;
        }

        public async Task<Livro> PostLivro(Livro livro)
        {
            if (livroValido(livro))
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(livro);
                    await transaction.CommitAsync();
                }
            }

            return livro;
        }

        public async Task<Livro> PutLivro(Livro livro, long id)
        {
            Livro _livro = GetLivro(id).Result;
            if (_livro != null && livro != null)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _livro.Quantidade = livro.Quantidade;
                    _livro.Estoque = livro.Estoque;
                    _livro.Autor.AutorId = livro.IdAutor.Value;

                    await _session.SaveOrUpdateAsync(_livro);
                    await transaction.CommitAsync();
                }

            }
            else
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _livro.Estoque--;
                    await _session.SaveOrUpdateAsync(_livro);
                    await transaction.CommitAsync();
                }

            }

            return livro != null ? livro : _livro; ;
        }

        private bool livroValido(Livro livro)
        {

            if (livro.Autor != null && livro.Quantidade != null)
                return true;

            return false;
        }
    }
}
