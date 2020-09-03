using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB.CNAE
{
    /// <summary>
    /// Seção -> Divisão -> Grupo -> Classe -> Subclasse -> Atividade Econômica
    /// </summary>
    public class CnaeSubclasse : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Descricao { get; set; }
        //public IList<string> Observacoes { get;}
        //public IList<string> Atividades { get;}
        public CnaeClasse Classe { get; set; }
    }
}
