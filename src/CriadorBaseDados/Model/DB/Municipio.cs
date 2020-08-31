using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Municipio : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
    }
}
