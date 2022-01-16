using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Estado : RealmObject
    {
        [PrimaryKey]
        public string UF { get; set; }
        [Backlink(nameof(Municipio.UF))]
        public IQueryable<Municipio> Municipios { get; }
    }
}
