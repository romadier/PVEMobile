using PVEM.Sessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PVEM.Paginas
{
    public class Sincronizar : ContentPage
    {
        public Sincronizar()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Content = new StackLayout
            {
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
                        Children = {
                            new Label {
                                Text = "Listar Questionários"
                            }
                        }

                    }

                }

            };
        }

    }
}