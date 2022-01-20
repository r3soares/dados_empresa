using CriadorBaseDados.Model.DB.CNAE;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Empresa : RealmObject
    {
        [JsonProperty]
        [PrimaryKey]
        public string CNPJ { get; set; }
        [JsonProperty]
        public bool IsMatriz { get; set; }
        [JsonProperty]
        public string RazaoSocial { get; set; }
        [JsonProperty]
        public string NomeFantasia { get; set; }
        [JsonProperty]
        public SituacaoCadastral Situacao { get; set; }
        [JsonProperty]
        public MotivoSituacao Motivo { get; set; }
        [JsonProperty]
        public int DataSituacao { get; set; }
        [JsonProperty]
        public NaturezaJuridica NaturezaJuridica { get; set; }
        [JsonProperty]
        public int DataInicioAtividade { get; set; }
        [JsonProperty]
        public CnaeSubclasse CNAE_Fiscal { get; set; }
        [JsonProperty]
        public Endereco Endereco { get; set; }
        [JsonProperty]
        public Municipio Municipio { get; set; }
        [JsonProperty]
        public Contato Contato { get; set; }
        [JsonProperty]
        public PorteEmpresa Porte { get; set; }
        [JsonProperty]
        public double CapitalSocial { get; set; }
        //[JsonProperty]
        [Backlink(nameof(SocioEmpresa.Empresa))]
        public IQueryable<SocioEmpresa> SociosEmpresa { get;}


    }
}
