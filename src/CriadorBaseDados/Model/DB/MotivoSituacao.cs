using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class MotivoSituacao : RealmObject
    {
        [PrimaryKey]
        public int Cod { get; set; }
        public string Descricao { get; set; }
    }
}
