using CRUD_Livraria.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Mapping
{
    public class AutorMap : ClassMap<Autor>
    {
        public AutorMap()
        {
            Id(l => l.AutorId).GeneratedBy.Identity();
            Map(l => l.Nome);
            Table("Autor");
        }
    }
}
