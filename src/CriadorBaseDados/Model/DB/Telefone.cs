using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Telefone : RealmObject
    {
        [PrimaryKey]
        public string Numero { get; set; }     
        public bool IsFax { get; set; }
    }
}
