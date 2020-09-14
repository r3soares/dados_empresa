using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CriadorBaseDados.Model.DB
{
    public class SocioEmpresa : RealmObject
    {
        public Socio Socio { get; set; }
        public Empresa Empresa { get; set; }
        public QualificacaoResponsavel Qualificacao { get; set; }
        public int DataEntrada { get; set; }
        public double Capital { get; set; }
    }
}
