using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Socio : RealmObject
    {
        [PrimaryKey]
        public string ID { get; set; }
        [Backlink(nameof(SocioEmpresa.Socio))]
        public IQueryable<SocioEmpresa> SocioEmpresas { get;}
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public TipoSocio Tipo { get; set; }
    }
}
