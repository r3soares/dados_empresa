using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class NaturezaJuridica : RealmObject
    {
        [PrimaryKey]
        public int Cod { get; set; }
        public GrupoNaturezaJuridica Grupo { get; set; }
        public string Descricao { get; set; }
    }
}
