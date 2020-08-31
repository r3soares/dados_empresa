using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    public class CnaeSecao : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Descricao { get; set; }
        public IList<string> Observacoes { get;}
        [Backlink(nameof(CnaeDivisao.Secao))]
        public IQueryable<CnaeDivisao> Divisoes { get; }
    }
}
