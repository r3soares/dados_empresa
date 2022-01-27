using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    /// <summary>
    /// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CnaeClasse : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int ID { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodGrupo => Grupo.ID;
        public CnaeGrupo Grupo { get; set; }
        [Backlink(nameof(CnaeSubclasse.Classe))]
        //[JsonProperty]
        public IQueryable<CnaeSubclasse> Subclasses { get; }
    }
}
