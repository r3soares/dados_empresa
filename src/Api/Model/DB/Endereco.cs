using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Endereco : RealmObject
    {
        [JsonProperty]
        public string Logradouro { get; set; }
        [JsonProperty]
        public string Numero { get; set; }
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Complemento { get; set; }
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Bairro { get; set; }
        [JsonProperty]
        public string CEP { get; set; }
    }
}
