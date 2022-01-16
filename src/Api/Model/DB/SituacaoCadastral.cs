using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    
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
        [PrimaryKey]
        public int Cod { get; set; }
        public string Descricao { get; set; }

    }
}
