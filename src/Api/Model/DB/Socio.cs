using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class Socio : RealmObject
    {
        [Backlink(nameof(SocioEmpresa.Socio))]
        public IQueryable<SocioEmpresa> SocioEmpresas { get;}
        [Indexed]
        public string Nome { get; set; }
        [Indexed]
        public string CNPJ_CPF { get; set; }
        public TipoSocio Tipo { get; set; }
    }
}
