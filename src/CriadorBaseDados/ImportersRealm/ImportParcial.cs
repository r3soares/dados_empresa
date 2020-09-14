using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CriadorBaseDados.ImportersRealm
{
    public class ImportParcial
    {
        const string ARQUIVO = "Arquivos/selecao_dados_realm.txt";
        const string NOME_DATABASE_PARCIAL = @"DatabasesRealm\";
        public void IniciaImport(Realm banco)
        {
            if (!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Arquivo não localizado:\n" + ARQUIVO);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            string[] dados = File.ReadAllLines(ARQUIVO);
            List<string> estados = new List<string>(dados[0].ToUpper().Split(';'));
            List<string> cnaes = new List<string>(dados[1].Split(';'));
            Console.WriteLine("Estados selecionados:");
            Console.WriteLine(dados[0].Replace(';', ' '));
            Console.WriteLine("Filtros CNAEs selecionados:");
            Console.WriteLine(dados[1]
                .Replace("s", "Seção ")
                .Replace("d", "Divisão ")
                .Replace("g", "Grupo ")
                .Replace("c", "Classe ")
                .Replace(';', '\n'));
            Console.WriteLine("Confirma estados e filtros? (s/n)");
            char opcao = Console.ReadKey().KeyChar;
            switch (opcao)
            {
                case 's':
                    var secoes = RemoveLetraString(cnaes.Where(s => s.StartsWith("s")).ToArray(), "s");
                    var divisoes = RemoveLetraInt(cnaes.Where(d => d.StartsWith("d")).ToArray(), "d");
                    var grupos = RemoveLetraInt(cnaes.Where(g => g.StartsWith("g")).ToArray(), "g");
                    var classes = RemoveLetraInt(cnaes.Where(c => c.StartsWith("c")).ToArray(), "c");
                    ImportRegiao ir = new ImportRegiao();
                    var cnaesSelecionados = ir.ImportCNAEs(secoes, divisoes, grupos, classes, banco).ToDictionary(x=>x,x=>x);
                    List<Empresa> empresas = new List<Empresa>();
                    foreach(var estado in estados)
                    {                        
                        var estadoAtual = ir.ImportEstado(estado, banco);
                        Realm baseEstado = Realm.GetInstance(NOME_DATABASE_PARCIAL + estadoAtual.UF + ".realm");
                        foreach (var municipio in estadoAtual.Municipios)
                        {
                            foreach(var empresa in municipio.Empresas)
                            {
                                if (empresa.CNAE_Fiscal == null)
                                    continue;
                                if(cnaesSelecionados.ContainsKey(empresa.CNAE_Fiscal.ID))
                                {
                                    empresas.Add(empresa);
                                    if(empresas.Count > 500000)
                                    {
                                        SalvaEmpresa(empresas, baseEstado);
                                    }
                                }
                            }
                        }
                        SalvaEmpresa(empresas, baseEstado);
                    }                    
                    
                    break;
                case 'n':
                    Console.WriteLine("Altere o arquivo " + ARQUIVO);
                    Console.WriteLine("... e reinicie o programa");
                    Console.Read();
                    Environment.Exit(0);
                    break;
            }
        }

        private void SalvaEmpresa(List<Empresa> empresas, Realm baseDados)
        {            
            baseDados.Write(() =>
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                foreach (var e in empresas)
                {
                    var json = JsonSerializer.Serialize(e,options);
                    Empresa e2 = JsonSerializer.Deserialize<Empresa>(json,options);
                    baseDados.Add(e2);
                }
            });
            empresas.Clear();
        }

        private int[] RemoveLetraInt(string[] grupo, string letra)
        {
            List<int> grupoInteiros = new List<int>();
            foreach(var texto in grupo)
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
