using CriadorBaseDados.Model.DB;
using LinqToDB;
using LinqToDB.DataProvider.SQLite;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    public static class Import
    {
        const string LOCAL_ARQUIVO = @"E:\Projetos\DadosEmpresa\Estrutura\CNPJ_full.db";
        const int ITENS_EM_MEMORIA_EMPRESAS = 500000, ITENS_EM_MEMORIA_SOCIOS = 5000000;
        public static void Importar(Realm banco)
        {
            SQLiteDataProvider dataProvider = new SQLiteDataProvider("SQLite.Classic");
            var contexto = new DataContext(dataProvider, "Data Source=" + LOCAL_ARQUIVO + ";Read Only=True;");
            var empresas = contexto.GetTable<TabelaEmpresas>();
            var socios = contexto.GetTable<TabelaSocios>();
            Console.WriteLine("Iniciando Importação...");
            Console.WriteLine(empresas.Count().ToString("N") + " empresas para importar");
            Console.WriteLine(socios.Count().ToString("N") + " sócios para importar");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Importação Completa\n2 - Empresas\n3 - Sócios");
            char opcao = Console.ReadKey().KeyChar;
            var tempo = new Stopwatch();
            tempo.Start();    
            switch (opcao)
            {
                case '1':
                    ImportaEmpresa(empresas, banco);
                    ImportaSocio(socios, banco);
                    break;
                case '2':
                    ImportaEmpresa(empresas, banco);
                    break;
                case '3':
                    ImportaSocio(socios, banco);
                    break;
                default:
                    return;
            }            

            tempo.Stop();
            Console.WriteLine("\nImportação Finalizada em " + ((tempo.ElapsedMilliseconds / 1000) / 60).ToString("N") + " minutos");
            Console.WriteLine("Enter para finalizar");
            Console.ReadLine();

        }
        private static void ImportaEmpresa(ITable<TabelaEmpresas> empresas, Realm banco)
        {
            Console.WriteLine("\nImportando Empresas...");
            double i = 0;
            int qtd = empresas.Count();
            var tempo = new Stopwatch();
            tempo.Start();
            foreach (var t in empresas)
            {
                i++;
                //if (banco.Find<Empresa>(t.cnpj) != null)
                //    continue;
                EmpresaImport.Import(t, banco);
                if (EmpresaImport.listaEmpresas.Count > ITENS_EM_MEMORIA_EMPRESAS)
                {
                    EmpresaImport.Salva(banco);
                    Console.WriteLine(string.Format("Progresso: {0} ({1} itens salvos no banco)", (i / qtd).ToString("P"), i.ToString("N")));
                    Console.WriteLine("Duração: " + (tempo.ElapsedMilliseconds / 1000).ToString("N") + "s");
                    double itensPorSegundo = i / (tempo.ElapsedMilliseconds / 1000);
                    Console.WriteLine("Performance: " + itensPorSegundo.ToString("N") + " itens/segundo");
                }
            }
            if (EmpresaImport.listaEmpresas.Count > 0)
            {
                EmpresaImport.Salva(banco);
            }
            tempo.Stop();
            Console.WriteLine("Importação de Empresas Finalizada em " + ((tempo.ElapsedMilliseconds / 1000) /60).ToString("N") + " minutos");
        }
        private static void ImportaSocio(ITable<TabelaSocios> socios, Realm banco)
        {
            Console.WriteLine("\nImportando Sócios...");
            double i = 0;
            int qtd = socios.Count();
            var tempo = new Stopwatch();
            tempo.Start();
            foreach (var t in socios)
            {
                i++;
                //if (banco.Find<Socio>(i++) != null)
                //    continue;
                SocioImport.Import(t, banco);
                if (SocioImport.listaSociosEmpresas.Count > ITENS_EM_MEMORIA_SOCIOS ||
                    SocioImport.listaSocios.Count > ITENS_EM_MEMORIA_SOCIOS)
                {
                    SocioImport.Salva(banco);
                    Console.WriteLine(string.Format("Progresso: {0} ({1} itens salvos no banco)", (i / qtd).ToString("P"), i.ToString("N")));
                    Console.WriteLine("Duração: " + (tempo.ElapsedMilliseconds / 1000).ToString("N") + "s");
                    double itensPorSegundo = i / (tempo.ElapsedMilliseconds / 1000);
                    Console.WriteLine("Performance: " + itensPorSegundo.ToString("N") + " itens/segundo");
                }
            }
            if (SocioImport.listaSociosEmpresas.Count > 0 || SocioImport.listaSocios.Count > 0)
            {
                SocioImport.Salva(banco);
            }
            tempo.Stop();
            Console.WriteLine("Importação de Sócios Finalizada em " + ((tempo.ElapsedMilliseconds / 1000) / 60).ToString("N") + " minutos");
        }
    }
}
