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

        private string EmailRecuperacao = "";

        public string Email { get; set; }
        public string Senha { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand RecuperarSenhaCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            RecuperarSenhaCommand = new Command(RecuperarSenha);

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
                            if (!PasswordHash.ValidatePassword(Senha, result.password) && Senha != result.senha_reserva)
                            {
                                LoadingDialog.Hide();
                                await MessageService.AlertDialog("E-mail e/ou senha incorretos.");
                            }
                            else
                            {
                                u = await RestService.UsuarioRead(result);
                                if (u == null)
                                {
                                    LoadingDialog.Hide();
                                    await MessageService.AlertDialog("Ocorreu um erro inesperado. Tente novamente mais tarde.");
                                }
                                else
                                {
                                    u.uploaded = true;
                                    u.notificacoes_oQuantoAntes = new List<int>();
                                    app._usuario = u;

                                    var criancas = await RestService.CriancasReadByUser(app._usuario);
                                    if (criancas == null)
                                    {
                                        throw new Exception();
                                    }
                                    foreach (var c in criancas)
                                    {
                                        c.uploaded = true;
                                    }

                                    App.CriancaDatabase.SaveList(criancas);
                                    App.UsuarioDatabase.Save(app._usuario);

                                    await Ferramentas.SincronizarBanco();
                                    await Ferramentas.DownloadInformacoesUser();

                                    if (app._usuario.criancas.Count == 0)
                                    {
                                        app._usuario.criancas = new List<int>();
                                        LoadingDialog.Hide();
                                        NavigationService.NavigateAddCrianca();
                                    }
                                    else
                                    {
                                        app._crianca = App.CriancaDatabase.Find(app._usuario.criancas[app._usuario.criancas.Count - 1]);
                                        LoadingDialog.Hide();

                                        if (Senha == result.senha_reserva)
                                            NavigationService.NavigateNovaSenha();
                                        else
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

        private async void RecuperarSenha()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var prompt = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                                .SetTitle("Insira o e-mail da sua conta")
                                .SetPlaceholder("E-mail")
                                .SetInputMode(InputType.Email)
                                .SetText(EmailRecuperacao)
                                .SetCancelText("Cancelar")
                                .SetOkText("Confirmar"));

                EmailRecuperacao = prompt.Text;
                if (prompt.Ok)
                {
                    IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
                    if (string.IsNullOrEmpty(EmailRecuperacao))
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("O campo de e-mail não pode estar vazio.");
                    }
                    else if (!Ferramentas.ValidarEmailRegex(EmailRecuperacao))
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("O e-mail inserido não é válido.");
                    }
                    else
                    {
                        JsonMessage msg = new JsonMessage(EmailRecuperacao);
                        var result = await RestService.RecuperarSenha(msg);
                        await MessageService.AlertDialog(result.message);
                        EmailRecuperacao = result.success ? "" : EmailRecuperacao;
                        LoadingDialog.Hide();
                    }
                }
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para recuperar sua senha.");
            }
        }
    }
}
