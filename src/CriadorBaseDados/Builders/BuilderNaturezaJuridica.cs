using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CriadorBaseDados.Builders
{
    public static class BuilderNaturezaJuridica
    {
        const string ARQUIVO = "Arquivos/natureza_juridica.txt";
        static public void Builder(Realm banco)
        {
            if (banco.All<NaturezaJuridica>().Any())
                return;
            if (!File.Exists(ARQUIVO))
            {
                Console.WriteLine("Arquivo não localizado:\n" + ARQUIVO);
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }

            List<GrupoNaturezaJuridica> grupo = new();
            List<NaturezaJuridica> natureza = new();
            bool isGrupo = true;
            int cod = 0;
            string desc = "";
            foreach (string linha in File.ReadAllLines(ARQUIVO))
            {
                if (string.IsNullOrEmpty(linha))
                {
                    isGrupo = false;
                    continue;
                }
                    
                string[] codDesc = linha.Split(';');
                cod = int.Parse(codDesc[0]);
                desc = codDesc[1];
                if (isGrupo)
                {                    
                    GrupoNaturezaJuridica qr = new()
                    {
                        Cod = cod,
                        Descricao = desc
                    };
                    grupo.Add(qr);
                }
                else
                {
                    NaturezaJuridica nj = new()
                    {
                        Cod = cod,
                        Descricao = desc,
                        Grupo = grupo.First(g => g.Cod == cod / 1000)
                    };
                    natureza.Add(nj);
                }
                
            }

            banco.Write(() =>
            {
                foreach(var g in grupo)
                {
                    banco.Add(g);
                }
                foreach(var n in natureza)
                {
                    banco.Add(n);
                }

            });
        }
    }
}
