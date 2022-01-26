using CriadorBaseDados.Model.DB.CNAE;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsMatriz { get; set; }
        [JsonProperty]
        public string RazaoSocial { get; set; }
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NomeFantasia { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodSituacao { get => Situacao.Cod; }
        public SituacaoCadastral Situacao { get; set; }
        [Ignored]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CodMotivo { get => Motivo?.Cod; }
        public MotivoSituacao Motivo { get; set; }
        [JsonProperty]
        public int DataSituacao { get; set; }
        [Ignored]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CodNaturezaJuridica { get => NaturezaJuridica?.Cod; }
        public NaturezaJuridica NaturezaJuridica { get; set; }
        [JsonProperty]
        public int DataInicioAtividade { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodCNAE_Fiscal { get => CNAE_Fiscal.ID; }
        public CnaeSubclasse CNAE_Fiscal { get; set; }
        [JsonProperty]
        public Endereco Endereco { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodMunicipio { get => Municipio.ID; }
        public Municipio Municipio { get; set; }
        [JsonProperty]
        public Contato Contato { get; set; }
        [Ignored]
        [JsonProperty]
        public int CodPorte { get => Porte.Cod; }
        public PorteEmpresa Porte { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double CapitalSocial { get; set; }
        //[JsonProperty]
        [Backlink(nameof(SocioEmpresa.Empresa))]
        public IQueryable<SocioEmpresa> SociosEmpresa { get;}


    }
}
