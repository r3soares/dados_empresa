using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
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
        [JsonProperty]
        public string Complemento { get; set; }
        [JsonProperty]
        public string Bairro { get; set; }
        [JsonProperty]
        public string CEP { get; set; }
    }
}
