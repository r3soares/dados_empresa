using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    [Table(Name = "socios")]
    public class TabelaSocios
    {
        [Column] public string cnpj { get; set; }
        [Column] public string cnpj_basico { get; set; }
        [Column] public string identificador_de_socio { get; set; }
        //[Column] public string tipo_socio { get; set; }
        [Column] public string nome_socio { get; set; }
        [Column] public string cnpj_cpf_socio { get; set; }
        [Column] public string qualificacao_socio { get; set; }
        //[Column] public string perc_capital { get; set; }
        [Column] public string data_entrada_sociedade { get; set; }
        [Column] public string pais { get; set; }
        //[Column] public string nome_pais_ext { get; set; }
        [Column] public string representante_legal { get; set; }
        [Column] public string nome_representante { get; set; }
        [Column] public string qualificacao_representante_legal { get; set; }
        [Column] public string faixa_etaria { get; set; }
    }
}
