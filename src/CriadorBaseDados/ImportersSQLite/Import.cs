using CriadorBaseDados.Model.DB;
using CriadorBaseDados.Model.DB.CNAE;
using LinqToDB;
using LinqToDB.DataProvider.SQLite;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    public class Import
    {
        const string LOCAL_ARQUIVO_FILTROS = @"Arquivos/selecao_dados_realm.txt";
        const string LOCAL_ARQUIVO = @"E:\Projetos\DadosEmpresa\Estrutura\CNPJ_full.db";
        const int ITENS_EM_MEMORIA_EMPRESAS = 1000000, ITENS_EM_MEMORIA_SOCIOS = 5000000;
        Dictionary<string, string> filtroCnaes;
        string[] estados;
        public void Importar(Realm banco)
        {
            //Define os filtros dos estados e CNAES
            ImportFiltros(banco);
            if(estados == null || filtroCnaes == null)
            {
                Console.WriteLine("Verificar filtros");
                Environment.Exit(0);
            }
            IniciaImportacaoSQL(banco);

        }

        private void IniciaImportacaoSQL(Realm banco)
        {
            SQLiteDataProvider dataProvider = new SQLiteDataProvider("SQLite.Classic");
            var contexto = new DataContext(dataProvider, "Data Source=" + LOCAL_ARQUIVO + ";Read Only=True;");
            var empresas = contexto.GetTable<TabelaEmpresas>().Where(x=> estados.Contains(x.uf) && filtroCnaes.ContainsKey(x.cnae_fiscal));
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

        private void ImportFiltros(Realm banco)
        {
            if (!File.Exists(LOCAL_ARQUIVO_FILTROS))
            {
                Console.WriteLine("Arquivo não localizado:\n" + LOCAL_ARQUIVO_FILTROS);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            string[] dados = File.ReadAllLines(LOCAL_ARQUIVO_FILTROS);
            //Define os estados para seleção no banco
            estados = dados[0].Split(';');
            List<string> cnaes = new List<string>(dados[1].Split(';'));
            Console.WriteLine("Estados e filtros CNAEs selecionados:");
            Console.WriteLine(dados[0].Replace(';', ' '));
            Console.WriteLine(dados[1]
                .Replace("s", "Seção ")
                .Replace("d", "Divisão ")
                .Replace("g", "Grupo ")
                .Replace("c", "Classe ")
                .Replace(';', '\n'));
            Console.WriteLine("Confirma filtros e estados? (s/n)");
            char opcao = Console.ReadKey().KeyChar;
            switch (opcao)
            {
                case 's':
                    var secoes = RemoveLetraString(cnaes.Where(s => s.StartsWith("s")).ToArray(), "s");
                    var divisoes = RemoveLetraInt(cnaes.Where(d => d.StartsWith("d")).ToArray(), "d");
                    var grupos = RemoveLetraInt(cnaes.Where(g => g.StartsWith("g")).ToArray(), "g");
                    var classes = RemoveLetraInt(cnaes.Where(c => c.StartsWith("c")).ToArray(), "c");
                    //Define o filtro para seleção no banco
                    filtroCnaes = ImportCNAEs(secoes, divisoes, grupos, classes, banco).ToDictionary(x => x, x => x);
                    break;
                case 'n':
                    Console.WriteLine("Altere o arquivo " + LOCAL_ARQUIVO_FILTROS);
                    Console.WriteLine("... e reinicie o programa");
                    Console.Read();
                    Environment.Exit(0);
                    break;
            }
        }

        private void ImportaEmpresa(IQueryable<TabelaEmpresas> empresas, Realm banco)
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
        private void ImportaSocio(ITable<TabelaSocios> socios, Realm banco)
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
        public List<string> ImportCNAEs(string[] secoes, int[] divisoes, int[] grupos, int[] classes, Realm baseCompleta)
        {
            List<string> subclasses = new List<string>();
            foreach (var s in secoes)
            {
                var temp = baseCompleta.Find<CnaeSecao>(s);
                if (temp != null)
                {
                    foreach (var d in temp.Divisoes)
                    {
                        foreach (var g in d.Grupos)
                        {
                            foreach (var c in g.Classes)
                            {
                                foreach (var sc in c.Subclasses)
                                {
                                    subclasses.Add(sc.ID.ToString());
                                }
                            }
                        }
                    }
                }
            }
            foreach (var d in divisoes)
            {
                var temp = baseCompleta.Find<CnaeDivisao>(d);
                if (temp != null)
                    foreach (var g in temp.Grupos)
                    {
                        foreach (var c in g.Classes)
                        {
                            foreach (var sc in c.Subclasses)
                            {
                                subclasses.Add(sc.ID.ToString());
                            }
                        }
                    }
            }
            foreach (var g in grupos)
            {
                var temp = baseCompleta.Find<CnaeGrupo>(g);
                if (temp != null)
                    foreach (var c in temp.Classes)
                    {
                        foreach (var sc in c.Subclasses)
                        {
                            subclasses.Add(sc.ID.ToString());
                        }
                    }
            }
            foreach (var c in classes)
            {
                var temp = baseCompleta.Find<CnaeClasse>(c);
                if (temp != null)
                    foreach (var sc in temp.Subclasses)
                    {
                        subclasses.Add(sc.ID.ToString());
                    }
            }

            return subclasses;
        }

        private int[] RemoveLetraInt(string[] grupo, string letra)
        {
            List<int> grupoInteiros = new List<int>();
            foreach (var texto in grupo)
            {
                var numeroTexto = texto.Replace(letra, "");
                grupoInteiros.Add(int.Parse(numeroTexto));
            }
            return grupoInteiros.ToArray();
        }
        private string[] RemoveLetraString(string[] grupo, string letra)
        {
            List<string> grupoStrings = new List<string>();
            foreach (var texto in grupo)
            {
                grupoStrings.Add(texto.Replace(letra, ""));
            }
            return grupoStrings.ToArray();
        }
    }
}
