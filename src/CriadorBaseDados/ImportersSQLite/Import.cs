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
        const string LOCAL_ARQUIVO = @"D:\Projetos\DadosEmpresa\cnpj.db";
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
            Console.WriteLine("\nObtendo dados da base SQLite...");
            SQLiteDataProvider dataProvider = new("SQLite.Classic");
            var contexto = new DataContext(dataProvider, "Data Source=" + LOCAL_ARQUIVO + ";Read Only=True;");
            var estabelecimentos = contexto.GetTable<TabelaEstabelecimentos>().Where(x=> estados.Contains(x.uf) && filtroCnaes.ContainsKey(x.cnae_fiscal));
            var cnpj = estabelecimentos.Select(x => x.cnpj_basico).ToList();
            var empresas = contexto.GetTable<TabelaEmpresas>().Where(x => cnpj.Contains(x.cnpj_basico));
            var socios = contexto.GetTable<TabelaSocios>();
            var municipios = contexto.GetTable<TabelaMunicipios>();
            Console.WriteLine("\nIniciando Importação...");
            //Console.WriteLine(estabelecimentos.Count().ToString("N") + " estabelecimentos para importar");
            //Console.WriteLine(empresas.Count().ToString("N") + " empresas para importar");
           // Console.WriteLine(socios.Count().ToString("N") + " sócios para importar");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Importação Completa\n2 - Empresas\n3 - Sócios");
            char opcao = Console.ReadKey().KeyChar;
            var tempo = new Stopwatch();
            tempo.Start();
            switch (opcao)
            {
                case '1':
                    ImportaEmpresa(estabelecimentos, empresas, municipios, banco);
                    ImportaSocio(socios, banco);
                    break;
                case '2':
                    ImportaEmpresa(estabelecimentos, empresas,municipios, banco);
                    break;
                case '3':
                    ImportaSocio(socios, banco);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    return;
            }

            tempo.Stop();
            Console.WriteLine("\nImportação Finalizada em " + ((tempo.ElapsedMilliseconds / 1000) / 60).ToString("N") + " minutos");
            Console.WriteLine("Enter para finalizar");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void ImportFiltros(Realm banco)
        {
            if (!File.Exists(LOCAL_ARQUIVO_FILTROS))
            {
                Console.WriteLine("\nArquivo não localizado:\n" + LOCAL_ARQUIVO_FILTROS);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            string[] dados = File.ReadAllLines(LOCAL_ARQUIVO_FILTROS);
            //Define os estados para seleção no banco
            estados = dados[0].Split(';');
            List<string> cnaes = new(dados[1].Split(';'));
            Console.WriteLine("\nEstados e filtros CNAEs selecionados:");
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
                    Console.WriteLine("\nAltere o arquivo " + LOCAL_ARQUIVO_FILTROS);
                    Console.WriteLine("... e reinicie o programa");
                    Console.Read();
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ImportaEmpresa(IQueryable<TabelaEstabelecimentos> estabelecimentos, IQueryable<TabelaEmpresas> empresas, IQueryable<TabelaMunicipios> municipios, Realm banco)
        {
            Console.WriteLine("\nImportando Empresas...");
            double i = 0;
            int qtd = estabelecimentos.Count();
            var mapEmpresas = empresas.ToDictionary(x=> x.cnpj_basico, x => x);
            var mapMunicipio = municipios.ToDictionary(x => int.Parse(x.codigo), x => x.descricao);
            var tempo = new Stopwatch();
            tempo.Start();
            foreach (var t in estabelecimentos)
            {
                i++;
                //if (banco.Find<Empresa>(t.cnpj) != null)
                //    continue;
                if(!mapEmpresas.TryGetValue(t.cnpj_basico, out TabelaEmpresas e))
                {
                    continue;
                }
                EmpresaImport.Import(t,e, mapMunicipio, banco);
                if (EmpresaImport.listaEmpresas.Count > ITENS_EM_MEMORIA_EMPRESAS)
                {
                    EmpresaImport.Salva(banco);
                    Console.WriteLine(string.Format("Progresso: {0} ({1} itens salvos no banco)", (i / qtd).ToString("P"), i.ToString("N")));
                    Console.WriteLine("Duração: " + (tempo.ElapsedMilliseconds / 1000).ToString("N") + "s");
                    double itensPorSegundo = i / (tempo.ElapsedMilliseconds / 1000);
                    Console.WriteLine("Performance: " + itensPorSegundo.ToString("N") + " itens/segundo");
                }
            }
            if (EmpresaImport.listaEmpresas.Any())
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
            if (SocioImport.listaSociosEmpresas.Any() || SocioImport.listaSocios.Any())
            {
                SocioImport.Salva(banco);
            }
            tempo.Stop();
            Console.WriteLine("Importação de Sócios Finalizada em " + ((tempo.ElapsedMilliseconds / 1000) / 60).ToString("N") + " minutos");
        }
        public static List<string> ImportCNAEs(string[] secoes, int[] divisoes, int[] grupos, int[] classes, Realm baseCompleta)
        {
            List<string> subclasses = new();
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

        private static int[] RemoveLetraInt(string[] grupo, string letra)
        {
            List<int> grupoInteiros = new();
            foreach (var texto in grupo)
            {
                var numeroTexto = texto.Replace(letra, "");
                grupoInteiros.Add(int.Parse(numeroTexto));
            }
            return grupoInteiros.ToArray();
        }
        private static string[] RemoveLetraString(string[] grupo, string letra)
        {
            List<string> grupoStrings = new();
            foreach (var texto in grupo)
            {
                grupoStrings.Add(texto.Replace(letra, ""));
            }
            return grupoStrings.ToArray();
        }
    }
}
