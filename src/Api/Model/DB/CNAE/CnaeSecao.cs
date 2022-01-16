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
    public class CnaeSecao : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public string ID { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
        [JsonProperty]
        //public string Observacoes { get; set; }
        [Backlink(nameof(CnaeDivisao.Secao))]
        public IQueryable<CnaeDivisao> Divisoes { get; }
    }
}
