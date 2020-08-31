using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    public class CnaeSubclasse : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Descricao { get; set; }
        public IList<string> Observacoes { get;}
        public IList<string> Atividades { get;}
        public CnaeClasse Classe { get; set; }
    }
}
