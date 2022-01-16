﻿using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NaturezaJuridica : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int Cod { get; set; }
        [JsonProperty]
        public GrupoNaturezaJuridica Grupo { get; set; }
        [JsonProperty]
        public string Descricao { get; set; }
    }
}