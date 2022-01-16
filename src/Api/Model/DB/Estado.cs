using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Estado : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public string UF { get; set; }
        [JsonProperty]
        [Backlink(nameof(Municipio.UF))]
        public IQueryable<Municipio> Municipios { get; }
    }
}
