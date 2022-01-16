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
    public class CnaeGrupo : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int ID { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
        [JsonProperty]
        public CnaeDivisao Divisao { get; set; }
        [JsonProperty]
        [Backlink(nameof(CnaeClasse.Grupo))]
        public IQueryable<CnaeClasse> Classes { get; }
    }
}
