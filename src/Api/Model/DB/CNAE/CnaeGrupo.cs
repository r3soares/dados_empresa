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
    public class CnaeGrupo : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Descricao { get; set; }
        //public IList<string> Observacoes { get;}
        public CnaeDivisao Divisao { get; set; }
        [Backlink(nameof(CnaeClasse.Grupo))]
        public IQueryable<CnaeClasse> Classes { get; }
    }
}
