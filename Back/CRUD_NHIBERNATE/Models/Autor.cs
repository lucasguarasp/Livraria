using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Models
{
    public class Autor
    {
        public virtual long AutorId { get; set; }
        public virtual string Nome { get; set; }
    }
}
