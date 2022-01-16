using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PorteEmpresa : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int Cod { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
    }
}
