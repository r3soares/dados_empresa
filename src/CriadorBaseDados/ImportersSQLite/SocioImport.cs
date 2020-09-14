using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    public static class SocioImport
    {
        static readonly public Dictionary<string, Socio> listaSocios = new Dictionary<string, Socio>();
        static readonly public List<SocioEmpresa> listaSociosEmpresas = new List<SocioEmpresa>();
        static public void Import(TabelaSocios tabela, Realm banco)
        {
            Empresa e = banco.Find<Empresa>(tabela.cnpj);
            if (e == null)
                return;
            string busca = tabela.nome_socio + tabela.cnpj_cpf_socio;
            Socio s = listaSocios.TryGetValue(busca, out s) ? s :
                banco.All<Socio>().FirstOrDefault(x => x.Nome.Equals(tabela.nome_socio) && x.CNPJ_CPF.Equals(tabela.cnpj_cpf_socio));                
            if(s == null)
            {
                s = new Socio
                {
                    CNPJ_CPF = tabela.cnpj_cpf_socio,
                    Tipo = banco.Find<TipoSocio>(ToInt(tabela.tipo_socio)),
                    Nome = tabela.nome_socio
                };
                listaSocios.Add(busca, s);
            }
            SocioEmpresa se = new SocioEmpresa
            {
                Capital = ToDouble(tabela.perc_capital),
                DataEntrada = ToInt(tabela.data_entrada),
                Empresa = e,
                Qualificacao = banco.Find<QualificacaoResponsavel>(ToInt(tabela.cod_qualificacao)),
                Socio = s
            };
            listaSociosEmpresas.Add(se);            
        }

        public static void Salva(Realm banco)
        {
            using (Transaction transaction = banco.BeginWrite())
            {
                foreach (var t in listaSocios.Values)
                {
                    banco.Add(t);
                }
                foreach (var t in listaSociosEmpresas)
                {
                    banco.Add(t);
                }
                transaction.Commit();
            }
            listaSocios.Clear();
            listaSociosEmpresas.Clear();
        }

        static int ToInt(string valor)
        {
            int.TryParse(valor, out int temp);
            return temp;
        }
        static double ToDouble(string valor)
        {
            double.TryParse(valor, out double temp);
            return temp;
        }
    }
}
