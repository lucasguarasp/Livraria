using CRUD_Livraria.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Mapping
{
    public class LivroMap : ClassMap<Livro>
    {
        public LivroMap()
        {
            Id(l => l.LivroId).GeneratedBy.Identity(); ;
            References(l => l.Autor);
            Map(l => l.Quantidade);
            Map(l => l.Estoque);
            Table("Livro");
        }
    }
}
