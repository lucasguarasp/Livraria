using CRUD_Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Interfaces.Gestores
{
    public interface ILivroGestor
    {
        Task<IEnumerable<Livro>> GetLivro();
        Task<Livro> GetLivro(long id);
        Task<Livro> PutLivro(Livro? livro, long id);
        bool LivroExists(long id);

        Task<Livro> PostLivro(Livro livro);
        Task<int> DeleteLivro(long id);


    }
}
