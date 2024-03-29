﻿using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class GrupoNaturezaJuridica : RealmObject
    {
        /// <summary>
        /// 1. Administração Pública
        /// 2. Entidades Empresariais
        /// 3. Entidades sem Fins Lucrativos
        /// 4. Pessoas Físicas
        /// 5. Organizações Internacionais e Outras Instituições Extraterritoriais
        /// </summary>
        [PrimaryKey]
        public int Cod { get; set; }
        public string Descricao { get; set; }
        [Backlink(nameof(NaturezaJuridica.Grupo))]
        public IQueryable<NaturezaJuridica> Lista { get; }
    }
}
