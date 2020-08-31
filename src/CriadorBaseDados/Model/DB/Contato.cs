using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Contato : RealmObject
    {
        /// <summary>
        /// CPF ou CNPJ do contato
        /// </summary>
        [PrimaryKey]
        public string ID { get; set; }
        [Backlink(nameof(Telefone.Contato))]
        public IQueryable<Telefone> Numeros { get; }
        public string Email { get; set; }
    }
}
