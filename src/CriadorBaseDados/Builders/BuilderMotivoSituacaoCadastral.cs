using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public static class BuilderMotivoSituacaoCadastral
    {
        const string ARQUIVO = "Arquivos/motivo_situacao_cadastral.txt";
        static public void Builder(Realm banco)
        {
            if (banco.All<MotivoSituacao>().Count() > 0)
                return;
            if (!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Arquivo não localizado:\n" + ARQUIVO);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }
            List<MotivoSituacao> lista = new List<MotivoSituacao>();
            foreach (string linha in File.ReadAllLines(ARQUIVO))
            {
                string[] codDesc = linha.Split(';');
                MotivoSituacao qr = new MotivoSituacao()
                {
                    Cod = int.Parse(codDesc[0]),
                    Descricao = codDesc[1]
                };
                lista.Add(qr);
            }
            banco.Write(() =>
            {
                foreach (MotivoSituacao s in lista)
                {
                    banco.Add(s);
                }
            });
        }
    }
}
