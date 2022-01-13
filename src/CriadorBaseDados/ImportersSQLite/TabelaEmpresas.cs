using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    [Table(Name = "empresas")]
    public class TabelaEmpresas
    {

        [Column] public string cnpj_basico { get; set; }
        [Column] public string razao_social { get; set; }
        [Column] public string natureza_juridica { get; set; }
        [Column] public string qualificacao_responsavel { get; set; }
        [Column] public string capital_social_str { get; set; }
        [Column] public string porte_empresa { get; set; }
        [Column] public string ente_federativo_responsavel { get; set; }
        [Column] public string capital_social { get; set; }

    }
}
