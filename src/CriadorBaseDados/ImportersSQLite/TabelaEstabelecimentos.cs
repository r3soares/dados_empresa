using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    [Table(Name = "estabelecimento")]
    public class TabelaEstabelecimentos
    {

        [Column] public string cnpj { get; set; }
        [Column] public string cnpj_basico { get; set; }
        [Column] public string cnpj_ordem { get; set; }
        [Column] public string cnpj_dv { get; set; }
        [Column] public string matriz_filial { get; set; }
        //[Column] public string razao_social { get; set; }
        [Column] public string nome_fantasia { get; set; }
        [Column] public string situacao_cadastral { get; set; }
        [Column] public string data_situacao_cadastral { get; set; }
        [Column] public string motivo_situacao_cadastral { get; set; }
        [Column] public string nome_cidade_exterior { get; set; }
        [Column] public string pais { get; set; }
        //[Column] public string nome_pais { get; set; }
        //[Column] public string cod_nat_juridica { get; set; }
        [Column] public string data_inicio_atividades { get; set; }
        [Column] public string cnae_fiscal { get; set; }
        [Column] public string cnae_fiscal_secundaria { get; set; }
        [Column] public string tipo_logradouro { get; set; }
        [Column] public string logradouro { get; set; }
        [Column] public string numero { get; set; }
        [Column] public string complemento { get; set; }
        [Column] public string bairro { get; set; }
        [Column] public string cep { get; set; }
        [Column] public string uf { get; set; }
        //[Column] public string cod_municipio { get; set; }
        [Column] public string municipio { get; set; }
        [Column] public string ddd1 { get; set; }
        [Column] public string telefone1 { get; set; }
        [Column] public string ddd2 { get; set; }
        [Column] public string telefone2 { get; set; }
        [Column] public string ddd_fax { get; set; }
        [Column] public string fax { get; set; }
        [Column] public string correio_eletronico { get; set; }
        //[Column] public string qualif_resp { get; set; }
        //[Column] public string capital_social { get; set; }
        //[Column] public string porte { get; set; }
        //[Column] public string opc_simples { get; set; }
        //[Column] public string data_opc_simples { get; set; }
        //[Column] public string opc_mei { get; set; }
        [Column] public string situacao_especial { get; set; }
        [Column] public string data_situacao_especial { get; set; }
    }
}
