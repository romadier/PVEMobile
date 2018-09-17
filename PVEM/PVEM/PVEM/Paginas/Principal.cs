using PVEM.Banco;
using PVEM.Sessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PVEM.Paginas
{
	public class Principal : ContentPage
	{
        private bool _executandoBotao = false;

		public Principal ()
		{

            NavigationPage.SetHasNavigationBar(this, false);
            Button btnQuestionarios = new Button
            {
                Text = "Questionarios",
            };

            btnQuestionarios.Clicked += ListarQuestionarios;

            Button btnSincronizar = new Button
            {
                Text = "Sincronizar",
            };

            btnSincronizar.Clicked += Sincronizar;

            int questionariosNaoRespondidos = (new AcessoBanco()).ListarRespostasNaoEnvidas().Count();

            string mensagem = "";

            if (questionariosNaoRespondidos == 1)
            {
                mensagem = "Existe 1 formulário pendente de envio, utilize a sincronização para que o mesmo seja enviado.";
            }
            else
            if (questionariosNaoRespondidos > 1)
            {
                mensagem = String.Format("Existem {0} formulários pendentes de sincronização, utilize a sincronização para que o mesmo seja enviado.", questionariosNaoRespondidos);
            }
            else
            {
                mensagem = String.Format("Não existe formulário pendentes de envio!", questionariosNaoRespondidos);
            }

            Content = new StackLayout {
				Children = {
                    new Image {
                        Source ="logo.png",
                        BackgroundColor = Color.FromHex("#3A3732"),
                        HorizontalOptions = LayoutOptions.Fill,
                        HeightRequest = 75
                    },
					new Label {
                        Text = Session.Instance.UsuarioLogado.Nome,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Margin = 10
                    },
                    new Label {
                        Text = Session.Instance.UsuarioLogado.Perfil,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        Margin = 10
                    },
                    new StackLayout {
                        Padding = 20,
                        Spacing = 20,
                        Children =
                        {
                            btnQuestionarios,
                            btnSincronizar
                        }
                    },
                    new Label {
                        Text = mensagem,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        Margin = 20
                    }

                }

            };

        }
        public void ListarQuestionarios(object sender, EventArgs args)
        {
            if (_executandoBotao)
            {
                return;
            }
            try
            {
                _executandoBotao = true;
                AcessoBanco banco = new AcessoBanco();

                DateTime? ultimaSincronizacao = banco.UltimaSincronizacao(Session.Instance.UsuarioLogado.IdAspNetUser);

                if (ultimaSincronizacao == null)
                {
                    DisplayAlert("Primeiro Acesso", "Você precisa realizar a sincronização dos dados para dar continuidade.", "OK");
                }
                else
                {
                    Navigation.PushAsync(new ListarQuestionarios());
                }

            }
            finally
            {
                _executandoBotao = false;
            }

            //App.Current.MainPage = new NavigationPage( new ListarQuestionarios());
        }

        public void Sincronizar(object sender, EventArgs args)
        {
            if (_executandoBotao)
            {
                return;
            }
            try
            {
                _executandoBotao = true;
                Navigation.PushAsync(new Paginas.Sincronizar());
            }
            finally
            {
                _executandoBotao = false;
            }
        }

    }
}