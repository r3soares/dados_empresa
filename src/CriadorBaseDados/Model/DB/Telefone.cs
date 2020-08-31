using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Telefone : RealmObject
    {
        /// <summary>
        /// Com DDD
        /// </summary>
        [PrimaryKey]
        public int Numero { get; set; }
        public bool IsFax { get; set; }
        public Contato Contato { get; set; }
    }
}
