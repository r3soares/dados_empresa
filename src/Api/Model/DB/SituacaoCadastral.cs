using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{

    [JsonObject(MemberSerialization.OptIn)]
    public class SituacaoCadastral : RealmObject
    {
        /// <summary>
        /// 2 DIGITOS
        //CÓDIGO DA SITUAÇÃO CADASTRAL
        //01 - NULA
        //02 - ATIVA
        //03 - SUSPENSA
        //04 - INAPTA
        //08 - BAIXADA
        /// </summary>
        [JsonProperty]
        [PrimaryKey]
        public int Cod { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }

    }
}
