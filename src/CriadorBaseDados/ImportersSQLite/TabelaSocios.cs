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
        [Column] public string tipo_socio { get; set; }
        [Column] public string nome_socio { get; set; }
        [Column] public string cnpj_cpf_socio { get; set; }
        [Column] public string cod_qualificacao { get; set; }
        [Column] public string perc_capital { get; set; }
        [Column] public string data_entrada { get; set; }
        [Column] public string cod_pais_ext { get; set; }
        [Column] public string nome_pais_ext { get; set; }
        [Column] public string cpf_repres { get; set; }
        [Column] public string nome_repres { get; set; }
        [Column] public string cod_qualif_repres { get; set; }
    }
}
