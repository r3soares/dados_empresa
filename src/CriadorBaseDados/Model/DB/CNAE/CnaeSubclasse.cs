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
    public class CnaeSubclasse : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        [Indexed]
        public string Descricao { get; set; }
        public CnaeClasse Classe { get; set; }

        [Backlink(nameof(Empresa.CNAE_Fiscal))]
        public IQueryable<Empresa> Empresas { get;}
    }
}
