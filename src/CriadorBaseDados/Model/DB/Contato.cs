using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Contato : RealmObject
    {
        public IList<Telefone> Numeros { get; }
        public string Email { get; set; }
    }
}
