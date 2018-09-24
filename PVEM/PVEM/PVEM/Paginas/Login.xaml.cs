using PVEM.Banco;
using PVEM.Servico;
using PVEM.Modelo;
using PVEM.Sessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVEM.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
            Ir.Clicked += BuscarUsuario;
        }

        public async void BuscarUsuario(object sender, EventArgs args)
        {
            try
            {
                Ir.IsEnabled = false;
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;

                await Task.Delay(500);
                string login = Email.Text;
                string senha = Senha.Text;
                AcessoBanco banco = new AcessoBanco();

                MobileUsuarioModel usuario = null;
                try
                {
                    usuario = UsuarioServico.BuscarUsuario(login, senha);
                }
                catch
                {
                    usuario = banco.ValidarSenhaUsuario(login, senha);
                }

                if (usuario != null)
                {
                    usuario.Senha = senha;
                    if (banco.UsuarioJaCadastrado(usuario.IdAspNetUser))
                    {
                        banco.AtualizarUsuario(usuario);
                    }
                    else
                    {
                        banco.IncluirUsuario(usuario);
                    }
                    Session.Instance.UsuarioLogado = usuario;
                    App.Current.MainPage = new NavigationPage(new Principal());

                }
                else
                {
                    activityIndicator.IsRunning = false;
                    await DisplayAlert("Erro ao validar usuário", "Usuário ou Senha Inválido!", "Ok");
                }
            }
            finally
            {
                Ir.IsEnabled = true;
                activityIndicator.IsVisible = false;
            }
        }

    }
}