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
        static readonly public List<Empresa> listaEmpresas = new();
        static readonly public Dictionary<string,Telefone> listaTelefones = new();
        static readonly public Dictionary<int,Municipio> listaMunicipios = new();
        static readonly public Dictionary<string, Estado> listaEstados = new();
        static public void Import(TabelaEstabelecimentos tabelaEstabelecimento, TabelaEmpresas tabelaEmpresa, Dictionary<int,string> mapMunicipios, Realm banco)
        {
            int situacao = ToInt(tabelaEstabelecimento.situacao_cadastral);
            if (situacao != 2)
                return;
            Empresa e = new()
            {
                CNPJ = tabelaEstabelecimento.cnpj,
                CapitalSocial = ToDouble(tabelaEmpresa.capital_social),
                CNAE_Fiscal = banco.Find<CnaeSubclasse>(int.Parse(tabelaEstabelecimento.cnae_fiscal)),
                Contato = CriaContato(tabelaEstabelecimento, banco),
                DataInicioAtividade = ToInt(tabelaEstabelecimento.data_inicio_atividades),
                DataSituacao = ToInt(tabelaEstabelecimento.data_situacao_cadastral),
                Endereco = CriaEndereco(tabelaEstabelecimento),
                IsMatriz = tabelaEstabelecimento.matriz_filial.Equals("1"),
                Motivo = banco.Find<MotivoSituacao>(ToInt(tabelaEstabelecimento.motivo_situacao_cadastral)),
                NaturezaJuridica = banco.Find<NaturezaJuridica>(ToInt(tabelaEmpresa.natureza_juridica)),
                NomeFantasia = tabelaEstabelecimento.nome_fantasia,
                Porte = banco.Find<PorteEmpresa>(ToInt(tabelaEmpresa.porte_empresa)),
                RazaoSocial = tabelaEmpresa.razao_social,
                Situacao = banco.Find<SituacaoCadastral>(situacao),
                Municipio = CriaPegaMunicipio(tabelaEstabelecimento, mapMunicipios, banco)
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

        static Contato CriaContato(TabelaEstabelecimentos tabela, Realm banco)
        {
            var c = new Contato
            {
                Email = tabela.correio_eletronico,
            };
            if (!string.IsNullOrEmpty(tabela.telefone1))
            {
                CriaSetTelefone(c, tabela.ddd1, tabela.telefone1, false, banco);
            }
            if (!string.IsNullOrEmpty(tabela.telefone2))
            {
                CriaSetTelefone(c, tabela.ddd2, tabela.telefone2, false, banco);
            }
            if (!string.IsNullOrEmpty(tabela.fax))
            {
                CriaSetTelefone(c, tabela.ddd_fax, tabela.fax, true, banco);
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

        static private Endereco CriaEndereco(TabelaEstabelecimentos tabela)
        {
            Endereco e = new()
            {
                Bairro = tabela.bairro,
                CEP = tabela.cep,
                Complemento = tabela.complemento,
                Logradouro = tabela.logradouro,
                Numero = tabela.numero,
            };
            return e;
        }

        static private Municipio CriaPegaMunicipio(TabelaEstabelecimentos tabela, Dictionary<int,string> mapMunicipios, Realm banco)
        {
            int id = ToInt(tabela.municipio);
            Municipio m = listaMunicipios.TryGetValue(id, out m) ? m : banco.Find<Municipio>(id);
            if (m == null)
            {
                m = new Municipio
                {
                    ID = id,
                    Nome = mapMunicipios[id],
                    UF = CriaPegaEstado(tabela, banco)
                };
                listaMunicipios.Add(id,m);
            }
            return m;
                
        }

        static private Estado CriaPegaEstado(TabelaEstabelecimentos tabela, Realm banco)
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
