using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    /// <summary>
    /// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
    /// </summary>
    public class CnaeSecao : RealmObject
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Descricao { get; set; }
        //public string Observacoes { get; set; }
        [Backlink(nameof(CnaeDivisao.Secao))]
        public IQueryable<CnaeDivisao> Divisoes { get; }
    }
}
