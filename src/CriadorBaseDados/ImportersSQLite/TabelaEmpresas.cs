using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    [Table(Name = "empresas")]
    public class TabelaEmpresas
    {

        [Column] public string cnpj { get; set; }
        [Column] public string matriz_filial { get; set; }
        [Column] public string razao_social { get; set; }
        [Column] public string nome_fantasia { get; set; }
        [Column] public string situacao { get; set; }
        [Column] public string data_situacao { get; set; }
        [Column] public string motivo_situacao { get; set; }
        [Column] public string nm_cidade_exterior { get; set; }
        [Column] public string cod_pais { get; set; }
        [Column] public string nome_pais { get; set; }
        [Column] public string cod_nat_juridica { get; set; }
        [Column] public string data_inicio_ativ { get; set; }
        [Column] public string cnae_fiscal { get; set; }
        [Column] public string tipo_logradouro { get; set; }
        [Column] public string logradouro { get; set; }
        [Column] public string numero { get; set; }
        [Column] public string complemento { get; set; }
        [Column] public string bairro { get; set; }
        [Column] public string cep { get; set; }
        [Column] public string uf { get; set; }
        [Column] public string cod_municipio { get; set; }
        [Column] public string municipio { get; set; }
        [Column] public string ddd_1 { get; set; }
        [Column] public string telefone_1 { get; set; }
        [Column] public string ddd_2 { get; set; }
        [Column] public string telefone_2 { get; set; }
        [Column] public string ddd_fax { get; set; }
        [Column] public string num_fax { get; set; }
        [Column] public string email { get; set; }
        [Column] public string qualif_resp { get; set; }
        [Column] public string capital_social { get; set; }
        [Column] public string porte { get; set; }
        [Column] public string opc_simples { get; set; }
        [Column] public string data_opc_simples { get; set; }
        [Column] public string opc_mei { get; set; }
        [Column] public string sit_especial { get; set; }
        [Column] public string data_sit_especial { get; set; }
    }
}
