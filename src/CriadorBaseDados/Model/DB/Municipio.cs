using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Municipio : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Nome { get; set; }
        public Estado UF { get; set; }
        [Backlink(nameof(Empresa.Municipio))]
        public IQueryable<Empresa> Empresas { get;}
    }
}
