using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Contato : RealmObject
    {
        [JsonProperty]
        public IList<Telefone> Numeros { get; }
        [JsonProperty]
        public string Email { get; set; }
    }
}
