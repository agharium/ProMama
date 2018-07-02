using Acr.UserDialogs;
using Plugin.Connectivity;
using ProMama.Components;
using ProMama.Components.Cryptography;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
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

        private bool LoginClicado = false;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        public async void Login()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (!LoginClicado)
                {
                    LoginClicado = true;

                    if (Email.Equals(string.Empty) || Senha.Equals(string.Empty))
                    {
                        await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
                        LoginClicado = false;
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
                                    await MessageService.AlertDialog("E-mail ou senha incorretos.");
                                    LoginClicado = false;
                                }
                                else
                                {
                                    u = await RestService.UsuarioRead(result);
                                    if (u == null)
                                    {
                                        await MessageService.AlertDialog("Algo de errado não está certo.");
                                        LoginClicado = false;
                                    }
                                    else
                                    {
                                        using (UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black))
                                        {
                                            app._usuario = u;
                                            App.UsuarioDatabase.Save(app._usuario);

                                            await Ferramentas.SincronizarBanco();
                                        }

                                        if (app._usuario.criancas.Count == 0)
                                        {
                                            NavigationService.NavigateAddCrianca();
                                        }
                                        else
                                        {
                                            app._crianca = app._usuario.criancas[app._usuario.criancas.Count - 1];
                                            NavigationService.NavigateHome();
                                        }
                                    }
                                }
                            } catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                                await MessageService.AlertDialog("E-mail ou senha incorretos.");
                            }
                        } else
                        {
                            await MessageService.AlertDialog("E-mail ou senha incorretos.");
                            LoginClicado = false;
                        }
                    }
                }
            } else {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para fazer login no aplicativo.");
            }
            
        }

    }
}
