using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Telefone : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public string Numero { get; set; }
        [JsonProperty]
        public bool IsFax { get; set; }
    }
}
