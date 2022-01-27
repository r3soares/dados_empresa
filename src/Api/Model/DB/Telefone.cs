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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PrimaryKey]
        public string Numero { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsFax { get; set; }
    }
}
