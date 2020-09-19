using CriadorBaseDados.Builders;
using CriadorBaseDados.ImportersRealm;
using CriadorBaseDados.ImportersSQLite;
using CriadorBaseDados.Model.DB;
using CriadorBaseDados.Model.DB.CNAE;
using Realms;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CriadorBaseDados
{
    class Program
    {
        static string NOME_DATABASE = @"E:\Projetos\CNPJ-full\DatabasesRealm\database_";

        static void Main()
        {
            //Console.WriteLine("Informe a opção desejada:");
            //Console.WriteLine("1 - Importação\n2 - Criar base de dados parciais");
            //char opcao = Console.ReadKey().KeyChar;
            //switch (opcao)
            //{
            //    case '1':
            //        Importar();
            //        break;
            //    case '2':
            //        CriaBaseParcial();
            //        break;
            //}
            Importar();
            
        }

        static void Importar()
        {
            Directory.CreateDirectory(@"E:\Projetos\CNPJ-full\DatabasesRealm");
            Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm");
            if (realm.All<CnaeSubclasse>().Count() == 0 || realm.All<TipoSocio>().Count() == 0)
            {
                BuilderCNAE.Builder(realm);
                BuilderMotivoSituacaoCadastral.Builder(realm);
                BuilderNaturezaJuridica.Builder(realm);
                BuilderPorte.Builder(realm);
                BuilderQualificacaoResponsavel.Builder(realm);
                BuilderSituacaoCadastral.Builder(realm);
                BuilderTipoSocio.Builder(realm);
            }
            new Import().Importar(realm);
        }

        static void CriaBaseParcial()
        {
            Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm");
            ImportParcial ip = new ImportParcial();
            ip.IniciaImport(realm);

        }
    }
}
