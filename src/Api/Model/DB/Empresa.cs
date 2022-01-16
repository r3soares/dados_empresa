using CriadorBaseDados.Model.DB.CNAE;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Empresa : RealmObject
    {
        [PrimaryKey]
        public string CNPJ { get; set; }
        public bool IsMatriz { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public SituacaoCadastral Situacao { get; set; }
        public MotivoSituacao Motivo { get; set; }
        public int DataSituacao { get; set; }
        public NaturezaJuridica NaturezaJuridica { get; set; }
        public int DataInicioAtividade { get; set; }
        public CnaeSubclasse CNAE_Fiscal { get; set; }
        public Endereco Endereco { get; set; }
        public Municipio Municipio { get; set; }
        public Contato Contato { get; set; }
        public PorteEmpresa Porte { get; set; }
        public double CapitalSocial { get; set; }
        [Backlink(nameof(SocioEmpresa.Empresa))]
        public IQueryable<SocioEmpresa> SociosEmpresa { get;}


    }
}
