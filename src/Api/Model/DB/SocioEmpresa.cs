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
        [Ignored]
        [JsonProperty]
        public string CNPJSocio => Socio.CNPJ_CPF;
        public Socio Socio { get; set; }
        [Ignored]
        [JsonProperty]
        public string CnpjEmpresa => Empresa.CNPJ;
        public Empresa Empresa { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodQualificacao => Qualificacao.Cod;
        public QualificacaoResponsavel Qualificacao { get; set; }
        [JsonProperty]
        public int DataEntrada { get; set; }
        [JsonProperty]
        public double Capital { get; set; }
    }
}
