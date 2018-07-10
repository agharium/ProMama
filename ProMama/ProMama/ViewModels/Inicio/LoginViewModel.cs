using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Inicio
{
    class LoginViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public string Email { get; set; }
        public string Senha { get; set; }

        public ICommand LoginCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        public async void Login()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            if (CrossConnectivity.Current.IsConnected)
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha))
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
                }
                else
                {
                    var u = new Usuario(Email, Senha);
                    var result  = await RestService.UsuarioLogin(u);
                        
                    if (result.success)
                    {
                        try
                        {
                            if (!PasswordHash.ValidatePassword(Senha, result.password))
                            {
                                LoadingDialog.Hide();
                                await MessageService.AlertDialog("E-mail ou senha incorretos.");
                            }
                            else
                            {
                                u = await RestService.UsuarioRead(result);
                                if (u == null)
                                {
                                    LoadingDialog.Hide();
                                    await MessageService.AlertDialog("Algo de errado não está certo.");
                                }
                                else
                                {
                                    app._usuario = u;
                                    App.UsuarioDatabase.Save(app._usuario);

                                    await Ferramentas.SincronizarBanco();
                                    await Ferramentas.DownloadInformacoesUser();

                                    if (app._usuario.criancas.Count == 0)
                                    {
                                        app._usuario.criancas = new List<Crianca>();
                                        LoadingDialog.Hide();
                                        NavigationService.NavigateAddCrianca();
                                    }
                                    else
                                    {
                                        app._crianca = app._usuario.criancas[app._usuario.criancas.Count - 1];
                                        LoadingDialog.Hide();
                                        NavigationService.NavigateHome();
                                    }
                                }
                            }
                        } catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                            LoadingDialog.Hide();
                            await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                        }
                    } else
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("E-mail ou senha incorretos.");
                    }
                }
            } else {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para fazer login no aplicativo.");
            }
        }
    }
}
