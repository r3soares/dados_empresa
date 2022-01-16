using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
    /// </summary>
    public class CnaeDivisao : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int ID { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
        [JsonProperty]
        public CnaeSecao Secao { get; set; }
        [JsonProperty]
        [Backlink(nameof(CnaeGrupo.Divisao))]
        public IQueryable<CnaeGrupo> Grupos { get; }
    }
}
