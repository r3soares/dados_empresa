using CriadorBaseDados.Model.DB;
using Newtonsoft.Json;
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
        const string PASTA_ESTADOS = @"DatabasesRealm\Estados\";
        private string[] estados = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", 
                                                  "MA", "MT", "MS", "MG", "PA", "PB", "PE", "PI", "PR", 
                                                  "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO", };
        public void IniciaImport(Realm banco)
        {
            if (!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Arquivo não localizado:\n" + ARQUIVO);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            string dados = File.ReadAllText(ARQUIVO);
            List<string> cnaes = new List<string>(dados.Split(';'));
            Console.WriteLine("Filtros CNAEs selecionados:");
            Console.WriteLine(dados
                .Replace("s", "Seção ")
                .Replace("d", "Divisão ")
                .Replace("g", "Grupo ")
                .Replace("c", "Classe ")
                .Replace(';', '\n'));
            Console.WriteLine("Confirma filtros? (s/n)");
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
                        Realm baseEstado = Realm.GetInstance(PASTA_ESTADOS + estadoAtual.UF + ".realm");
                        foreach (var municipio in estadoAtual.Municipios)
                        {
                            foreach(var empresa in municipio.Empresas)
                            {
                                empresas.Add(empresa);
                                if (empresas.Count > 500000)
                                {
                                    SalvaEmpresa(empresas, baseEstado);
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
                //var options = new JsonSerializerSettings
                //{
                //    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //};
                foreach (var e in empresas)
                {
                    //var json = JsonSerializer.Serialize(e,options);
                    //Empresa e2 = JsonSerializer.Deserialize<Empresa>(json,options);
                    //var json = JsonConvert.SerializeObject(e, options);
                    //Empresa e2 = JsonConvert.DeserializeObject<Empresa>(json,options);
                    baseDados.Add(e);
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
