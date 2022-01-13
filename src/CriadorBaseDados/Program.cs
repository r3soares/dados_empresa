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
        static readonly string NOME_DATABASE = @"D:\Projetos\DadosEmpresa\base_realm\cnpj";

        static void Main()
        {
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Importação\n2 - Compactar\n3 - Deletar os Sem Contato");
            char opcao = Console.ReadKey().KeyChar;
            switch (opcao)
            {
                case '1':
                    Importar();
                    DeleteCNAESVazios();
                    DeleteSemEmailTelefone();
                    break;
                case '2':
                    Compactar();
                    break;
                case '3':
                    DeleteSemEmailTelefone();
                    break;
                default:
                    Console.WriteLine("\nOpcão inválida");
                    break;

            }
            
        }

        private static void DeleteSemEmailTelefone()
        {
            using (Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm"))
            {
                List<Empresa> lista = new();
                foreach (var e in realm.All<Empresa>())
                {
                    if(e.Contato == null || string.IsNullOrEmpty(e.Contato.Email) || e.Contato.Numeros.Count == 0)
                    {
                        lista.Add(e);
                    }
                }
                realm.Write(() =>
                {
                    foreach (var c in lista)
                        realm.Remove(c);
                });
            }
            RealmConfiguration configuration = new(NOME_DATABASE + ".realm");
            Realm.Compact(configuration);
        }

        static void Importar()
        {
            Console.WriteLine("\nCriando CNAEs...");
            //Directory.CreateDirectory(@"E:\Projetos\CNPJ-full\DatabasesRealm");
            Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm");
            if (!realm.All<CnaeSubclasse>().Any() || !realm.All<TipoSocio>().Any())
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

        static void DeleteCNAESVazios()
        {
            using (Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm"))
            {
                List<CnaeSubclasse> lista = new();
                foreach (var cnae in realm.All<CnaeSubclasse>())
                {
                    if (!cnae.Empresas.Any())
                    {
                        lista.Add(cnae);
                    }
                }
                realm.Write(() =>
                {
                    foreach (var c in lista)
                        realm.Remove(c);
                });
            }
            RealmConfiguration configuration = new(NOME_DATABASE + ".realm");
            Realm.Compact(configuration);
        }

        static void Compactar()
        {
            Console.WriteLine("\nCompactando..");
            RealmConfiguration configuration = new(NOME_DATABASE + ".realm");
            Realm.Compact(configuration);
            Console.WriteLine("\nCompactação finalizada");
        }

        static void CriaBaseParcial()
        {
            Realm realm = Realm.GetInstance(NOME_DATABASE + ".realm");
            ImportParcial ip = new();
            ip.IniciaImport(realm);

        }
    }
}
