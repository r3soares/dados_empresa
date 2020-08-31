using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    public class CnaeClasse : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Descricao { get; set; }
        public IList<string> Observacoes { get;}
        public CnaeGrupo Grupo { get; set; }
        [Backlink(nameof(CnaeSubclasse.Classe))]
        public IQueryable<CnaeSubclasse> Subclasses { get; }
    }
}
