using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class QualificacaoResponsavel : RealmObject
    {
        [PrimaryKey]
        public int Cod { get; set; }
        public string Descricao { get; set; }
    }
}
