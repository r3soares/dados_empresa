using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public static class BuilderPorte
    {
        static public void Builder(Realm banco)
        {
            if (banco.All<PorteEmpresa>().Any())
                return;

            List<PorteEmpresa> lista = new()
            {
                new PorteEmpresa(){Cod = 0, Descricao = "Não Informado"},
                new PorteEmpresa(){Cod = 1, Descricao = "Micro Empresa"},
                new PorteEmpresa(){Cod = 3, Descricao = "Empresa de Pequeno Porte"},
                new PorteEmpresa(){Cod = 5, Descricao = "Demais"},
            };
            banco.Write(() =>
            {
                foreach (PorteEmpresa s in lista)
                {
                    banco.Add(s);
                }
            });
        }
    }
}
