using CRUD_Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Interfaces.Gestores
{
    public interface IAutorGestor
    {
        Task<IEnumerable<Autor>> GetAutor();
        Task<Autor> GetAutor(long id);
        Task<Autor> PutAutor(Autor autor, long id);
        bool AutorExists(long id);

        Task<Autor> PostAutor(Autor livro);
        Task<int> DeleteAutor(long id);
    }
}
