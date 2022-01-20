﻿using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Municipio : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public int ID { get; set; }
        [JsonProperty]
        public string Nome { get; set; }
        [JsonProperty]
        public Estado UF { get; set; }
        //[JsonProperty]
        [Backlink(nameof(Empresa.Municipio))]
        public IQueryable<Empresa> Empresas { get;}
    }
}
