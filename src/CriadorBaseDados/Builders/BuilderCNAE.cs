using CriadorBaseDados.Auxiliares;
using CriadorBaseDados.Model.DB.CNAE;
using Realms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CriadorBaseDados.Builders
{
    public class BuilderCNAE
    {
        static string ARQUIVO = "Arquivos/cnae.json";
        static public void Builder(Realm banco)
        {
            var jsonstring = File.ReadAllText(ARQUIVO);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            serializeOptions.Converters.Add(new Int32Converter());
            List<CnaeSubclasse> subclasses = JsonSerializer.Deserialize<List<CnaeSubclasse>>(jsonstring, serializeOptions);
            var porSecao = subclasses.GroupBy(x => x.Classe.Grupo.Divisao.Secao.ID)
                .Select(y => y.First())
                .Select(z => z.Classe.Grupo.Divisao.Secao)
                .OrderBy(x => x.ID);
            var porDivisao = subclasses.GroupBy(x => x.Classe.Grupo.Divisao.ID)
                .Select(y => y.First())
                .Select(z => z.Classe.Grupo.Divisao)
                .OrderBy(x => x.ID);
            var porGrupo = subclasses.GroupBy(x => x.Classe.Grupo.ID)
                .Select(y => y.First())
                .Select(z => z.Classe.Grupo)
                .OrderBy(x => x.ID);
            var porClasse = subclasses.GroupBy(x => x.Classe.ID)
                .Select(y => y.First())
                .Select(z => z.Classe)
                .OrderBy(x => x.ID);
            //Teste
            var config = new InMemoryConfiguration("temp");
            banco = Realm.GetInstance(config);

            banco.Write(() =>
            {
                //foreach(var s in porSecao)
                //{
                //    //Seção
                //    banco.Add(s);
                //}
                //foreach (var s in porDivisao)
                //{
                //    //Divisão
                //    banco.Add(s);
                //}
                //foreach (var s in porGrupo)
                //{
                //    //Grupo
                //    banco.Add(s);
                //}
                //foreach (var s in porClasse)
                //{
                //    //Classe
                //    banco.Add(s);
                //}
                foreach (var s in subclasses)
                {
                    //Subclasse
                    banco.Add(s, true);
                }
            });
        }
    }
}
