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
    public class CnaeSubclasse : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int ID { get; set; }
        [JsonProperty]
        [Indexed]
        public string Descricao { get; set; }
        [JsonProperty]
        public CnaeClasse Classe { get; set; }

        [JsonProperty]
        [Backlink(nameof(Empresa.CNAE_Fiscal))]
        public IQueryable<Empresa> Empresas { get;}
    }
}
