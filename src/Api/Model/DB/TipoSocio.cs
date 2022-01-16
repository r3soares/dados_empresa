using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TipoSocio : RealmObject
    {
        /// <summary>
        /// 1 – PESSOA JURÍDICA
        //  2 – PESSOA FISICA
        //  3 – ESTRANGEIRO
        /// </summary>
        [JsonProperty]
        [PrimaryKey]
        public int Cod { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
    }
}
