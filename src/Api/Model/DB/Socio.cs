using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Socio : RealmObject
    {
       // [JsonProperty]
        [Backlink(nameof(SocioEmpresa.Socio))]
        public IQueryable<SocioEmpresa> SocioEmpresas { get;}
        [JsonProperty]
        [Indexed]
        public string Nome { get; set; }
        [JsonProperty]
        [Indexed]
        public string CNPJ_CPF { get; set; }
        [JsonProperty]
        public TipoSocio Tipo { get; set; }
    }
}
