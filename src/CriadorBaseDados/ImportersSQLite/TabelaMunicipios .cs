using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    [Table(Name = "municipio")]
    public class TabelaMunicipios
    {

        [Column] public string codigo { get; set; }
        [Column] public string descricao { get; set; }

    }
}
