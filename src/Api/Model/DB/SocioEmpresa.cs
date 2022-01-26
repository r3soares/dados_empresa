using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SocioEmpresa : RealmObject
    {
        [JsonProperty]
        public Socio Socio { get; set; }
        //[JsonProperty]
        public Empresa Empresa { get; set; }
        [JsonProperty]
        public QualificacaoResponsavel Qualificacao { get; set; }
        [JsonProperty]
        public int DataEntrada { get; set; }
        [JsonProperty]
        public double Capital { get; set; }
    }
}
