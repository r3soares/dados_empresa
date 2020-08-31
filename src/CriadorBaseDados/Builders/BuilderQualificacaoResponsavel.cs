using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public class BuilderQualificacaoResponsavel
    {
        static string ARQUIVO = "Arquivos/qualificacao_responsavel.txt";
        static public void Builder(Realm banco)
        {
            if (banco.All<QualificacaoResponsavel>().Count() > 0)
                return;
            if(!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Arquivo não localizado:\n" + ARQUIVO);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            List<QualificacaoResponsavel> lista = new List<QualificacaoResponsavel>();
            foreach(string linha in File.ReadAllLines(ARQUIVO))
            {
                string[] codDesc = linha.Split(';');
                QualificacaoResponsavel qr = new QualificacaoResponsavel()
                {
                    Cod = int.Parse(codDesc[0]),
                    Descricao = codDesc[1]
                };
                lista.Add(qr);
            }
            banco.Write(() =>
            {
                foreach (QualificacaoResponsavel s in lista)
                {
                    banco.Add(s);
                }
            });
        }
    }
}
