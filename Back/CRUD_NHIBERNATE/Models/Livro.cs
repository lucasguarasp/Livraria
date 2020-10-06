using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Models
{
    public class Livro
    {
        public virtual long LivroId { get; set; }
        public virtual long? IdAutor { get { return Autor?.AutorId; } }
        //[JsonIgnore]
        public virtual Autor Autor { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual long Estoque { get; set; }
            
    }
}
