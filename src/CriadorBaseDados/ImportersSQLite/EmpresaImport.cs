using CriadorBaseDados.Model.DB;
using CriadorBaseDados.Model.DB.CNAE;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.ImportersSQLite
{
    public static class EmpresaImport
    {
        static readonly public List<Empresa> listaEmpresas = new List<Empresa>();
        static readonly public Dictionary<string,Telefone> listaTelefones = new Dictionary<string, Telefone>();
        static readonly public Dictionary<int,Municipio> listaMunicipios = new Dictionary<int, Municipio>();
        static readonly public Dictionary<string, Estado> listaEstados = new Dictionary<string, Estado>();
        static public void Import(TabelaEmpresas tabela, Realm banco)
        {
            int situacao = ToInt(tabela.situacao);
            if (situacao != 2)
                return;
            Empresa e = new Empresa
            {
                CNPJ = tabela.cnpj,
                CapitalSocial = ToDouble(tabela.capital_social),
                CNAE_Fiscal = banco.Find<CnaeSubclasse>(int.Parse(tabela.cnae_fiscal)),
                Contato = CriaContato(tabela, banco),
                DataInicioAtividade = ToInt(tabela.data_inicio_ativ),
                DataSituacao = ToInt(tabela.data_situacao),
                Endereco = CriaEndereco(tabela),
                IsMatriz = tabela.matriz_filial.Equals("1"),
                Motivo = banco.Find<MotivoSituacao>(ToInt(tabela.motivo_situacao)),
                NaturezaJuridica = banco.Find<NaturezaJuridica>(ToInt(tabela.cod_nat_juridica)),
                NomeFantasia = tabela.nome_fantasia,
                Porte = banco.Find<PorteEmpresa>(ToInt(tabela.porte)),
                RazaoSocial = tabela.razao_social,
                Situacao = banco.Find<SituacaoCadastral>(situacao),
                Municipio = CriaPegaMunicipio(tabela, banco)
            };
            listaEmpresas.Add(e);
        }

        public static void Salva(Realm banco)
        {
            using (Transaction transaction = banco.BeginWrite())
            {
                foreach (var t in listaMunicipios.Values)
                {
                    banco.Add(t);
                }
                foreach (var t in listaTelefones.Values)
                {
                    banco.Add(t);
                }
                foreach (var t in listaEmpresas)
                {
                    banco.Add(t);
                }                
                transaction.Commit();
            }
            listaTelefones.Clear();
            listaEmpresas.Clear();
            listaMunicipios.Clear();
        }

        static Contato CriaContato(TabelaEmpresas tabela, Realm banco)
        {
            var c = new Contato
            {
                Email = tabela.email,
            };
            if (!string.IsNullOrEmpty(tabela.telefone_1))
            {
                CriaSetTelefone(c, tabela.ddd_1, tabela.telefone_1, false, banco);
            }
            if (!string.IsNullOrEmpty(tabela.telefone_2))
            {
                CriaSetTelefone(c, tabela.ddd_2, tabela.telefone_2, false, banco);
            }
            if (!string.IsNullOrEmpty(tabela.num_fax))
            {
                CriaSetTelefone(c, tabela.ddd_fax, tabela.num_fax, true, banco);
            }
            return c;
        }

        static private void CriaSetTelefone(Contato c, string ddd, string numero, bool isFax, Realm banco)
        {
            string dddNumero = ddd + numero;
            //Verifica se o número já existe na lista ou no banco
            Telefone t = null;
            t = listaTelefones.TryGetValue(dddNumero, out t) ? t : banco.Find<Telefone>(dddNumero);
            if(t == null)
            {
               t = new Telefone
                {
                    Numero = dddNumero,
                    IsFax = isFax
                };
                //Se não existir, adiciona a lista
                listaTelefones.Add(t.Numero, t);
            }
            //Verifica se o numero já existe no Contato
            if (c.Numeros.FirstOrDefault(x => x.Numero.Equals(t.Numero)) == null)
            {
                //Se não existir, adiciona
                c.Numeros.Add(t);
            }
        }

        static private Endereco CriaEndereco(TabelaEmpresas tabela)
        {
            Endereco e = new Endereco
            {
                Bairro = tabela.bairro,
                CEP = tabela.cep,
                Complemento = tabela.complemento,
                Logradouro = tabela.logradouro,
                Numero = tabela.numero,
            };
            return e;
        }

        static private Municipio CriaPegaMunicipio(TabelaEmpresas tabela, Realm banco)
        {
            int id = ToInt(tabela.cod_municipio);
            Municipio m = listaMunicipios.TryGetValue(id, out m) ? m : banco.Find<Municipio>(id);
            if (m == null)
            {
                m = new Municipio
                {
                    ID = id,
                    Nome = tabela.municipio,
                    UF = CriaPegaEstado(tabela, banco)
                };
                listaMunicipios.Add(id,m);
            }
            return m;
                
        }

        static private Estado CriaPegaEstado(TabelaEmpresas tabela, Realm banco)
        {
            listaEstados.TryGetValue(tabela.uf.ToUpper(), out Estado e);
            if(e == null)
            {
                banco.Write(() =>
                {
                    e = new Estado
                    {
                        UF = tabela.uf.ToUpper()
                    };
                    listaEstados.Add(e.UF, banco.Add(e));
                });
            }
            return e;
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
