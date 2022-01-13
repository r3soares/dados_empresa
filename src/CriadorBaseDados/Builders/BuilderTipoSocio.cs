using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public static class BuilderTipoSocio
    {
        static public void Builder(Realm banco)
        {
            if (banco.All<TipoSocio>().Any())
                return;

            List<TipoSocio> lista = new()
            {
                new TipoSocio(){Cod = 1, Descricao = "Jurídica"},
                new TipoSocio(){Cod = 2, Descricao = "Física"},
                new TipoSocio(){Cod = 3, Descricao = "Estrangeiro"},
            };
            banco.Write(() =>
            {
                foreach (TipoSocio s in lista)
                {
                    banco.Add(s);
                }
            });
        }
    }
}
