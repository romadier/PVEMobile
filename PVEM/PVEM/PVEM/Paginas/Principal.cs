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

            Content = new StackLayout {
				Children = {
                    new Image {
                        Source ="logo2.png",
                        BackgroundColor = Color.FromHex("#3A3732"),
                        HorizontalOptions = LayoutOptions.Fill
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
                    }
                    
                }

            };

        }
        public void ListarQuestionarios(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ListarQuestionarios());
            //App.Current.MainPage = new NavigationPage( new ListarQuestionarios());
        }
        public void Sincronizar(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Paginas.Sincronizar());
            //App.Current.MainPage = new NavigationPage( new Paginas.Sincronizar());
        }

    }
}