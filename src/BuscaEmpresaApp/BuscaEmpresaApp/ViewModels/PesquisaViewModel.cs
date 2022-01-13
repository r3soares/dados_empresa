using CriadorBaseDados.Model.DB;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuscaEmpresaApp.ViewModels
{
    public class PesquisaViewModel : BaseViewModel
    {
        public PesquisaViewModel()
        {
            Title = "Pesquisa";
            PesquisarCommand = new Command(Pesquisa);
            CancelarCommand = new Command(() => cts?.Cancel());
        }

        public IList<Estado> UFs => Estados.ToList();
        public Estado EstadoSelecionado { get; set; }

        public string TermoPesquisa { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ObservableCollection<Empresa> ResultadoPesquisa { get; set; } = new ObservableCollection<Empresa>();

        public bool IsOcupado { get; set; }
        private CancellationTokenSource cts;

        private async void Pesquisa(object obj)
        {
            if (string.IsNullOrEmpty(TermoPesquisa) || EstadoSelecionado == null)
                return;
            ResultadoPesquisa.Clear();
            IsOcupado = true;
            if (TermoPesquisa.Length == 14)
            {
                var e = GetEmpresa(TermoPesquisa);
                if (e != null)
                {
                    ResultadoPesquisa.Add(e);
                    IsOcupado = false;
                    return;
                }
            }
            IProgress<string> reportEmpresa = new Progress<string>(AddEmpresa);
            string uf = EstadoSelecionado.UF;
            List<string> termos = new List<string>(TermoPesquisa.Split(' '));
            termos.RemoveAll(t => t.Length <= 2);
            using (cts = new CancellationTokenSource())
            {
                await Task.Run(() =>
                {
                    var estado = GetEstado(uf);
                    foreach (var municipio in estado.Municipios)
                    {
                        foreach (var empresa in municipio.Empresas)
                        {
                            if(empresa.Contato == null || empresa.Contato.Numeros.Count == 0 && string.IsNullOrEmpty(empresa.Contato.Email))
                            {
                                continue;
                            }
                            //Verifica se todos os termos da pesquisa foram encontrados
                            bool isTermoCompleto = false;
                            foreach (var t in termos)
                            {
                                if (empresa.CNAE_Fiscal.Descricao.Contains(t, StringComparison.CurrentCultureIgnoreCase) ||
                                empresa.RazaoSocial.Contains(t, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    isTermoCompleto = true;
                                    continue;
                                }
                                isTermoCompleto = false;
                            }
                            if (isTermoCompleto)
                                reportEmpresa.Report(empresa.CNPJ);
                            if (cts.IsCancellationRequested)
                                return;
                        }
                    }
                });
                IsOcupado = false;
            }
        }

        private void AddEmpresa(string obj)
        {
            ResultadoPesquisa.Add(GetEmpresa(obj));
        }
    }
}