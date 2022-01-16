using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class TipoSocio : RealmObject
    {
        /// <summary>
        /// 1 – PESSOA JURÍDICA
        //  2 – PESSOA FISICA
        //  3 – ESTRANGEIRO
        /// </summary>
        [PrimaryKey]
        public int Cod { get; set; }
        public string Descricao { get; set; }
    }
}
