using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public static class BuilderSituacaoCadastral
    {
        static public void Builder(Realm banco)
        {
            if (banco.All<SituacaoCadastral>().Any())
                return;

            List<SituacaoCadastral> lista = new()
            {
                new SituacaoCadastral(){Cod = 1, Descricao = "Nula"},
                new SituacaoCadastral(){Cod = 2, Descricao = "Ativa"},
                new SituacaoCadastral(){Cod = 3, Descricao = "Suspensa"},
                new SituacaoCadastral(){Cod = 4, Descricao = "Inapta"},
                new SituacaoCadastral(){Cod = 8, Descricao = "Baixada"},
            };
            banco.Write(() =>
            {
                foreach(SituacaoCadastral s in lista)
                {
                    banco.Add(s);
                }
            });
        }
    }
}
