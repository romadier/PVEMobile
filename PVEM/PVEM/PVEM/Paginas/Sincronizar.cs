using PVEM.Banco;
using PVEM.Modelo;
using PVEM.Servico;
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
        private Label lblUltimaSincronizacao;

        public Sincronizar()
        {
            AcessoBanco banco = new AcessoBanco();
            NavigationPage.SetHasNavigationBar(this, false);

            DateTime? ultimaSincronizacao = banco.UltimaSincronizacao(Session.Instance.UsuarioLogado.IdAspNetUser);

            string strUltimaSincronizacao = "Não houve";

            if (ultimaSincronizacao != null)
                strUltimaSincronizacao = ultimaSincronizacao.Value.ToString("dd/MM/yyyy HH:mm:ss");

            Button btnSincronizar = new Button
            {
                Text = "Sincronizar",
            };
            lblUltimaSincronizacao = new Label
            {
                Text = strUltimaSincronizacao,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                Margin = 10
            };

            Content = new StackLayout
            {
                Children = {
                    new Image {
                        Source ="logo2.png",
                        BackgroundColor = Color.FromHex("#3A3732"),
                        HorizontalOptions = LayoutOptions.Fill
                    },
                    new StackLayout {
                        Padding = 20,
                        Spacing = 20,
                        Children = {
                            new Label {
                                Text = "Ultima Sincronização",
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                Margin = 10
                            },
                            lblUltimaSincronizacao,
                            btnSincronizar
                        }
                    }
                }
            };

            btnSincronizar.Clicked += AcaoSincronizar;

        }

        public void AcaoSincronizar(object sender, EventArgs args)
        {
            try
            {
                AcessoBanco banco = new AcessoBanco();

                string usuario = Session.Instance.UsuarioLogado.IdAspNetUser;

                List<long> questionarios = UsuarioServico.PegarQuestionariosUsuario(usuario);

                banco.GravarQuestionariosUsuario(usuario, questionarios);

                foreach (var item in questionarios)
                {
                    if (!banco.ExisteQuestionario(item))
                    {
                        RespostaQuestionarioForm rqf = UsuarioServico.BaixarQuestionario(item);

                        banco.GravarQuestionario(rqf);
                    }
                }
                lblUltimaSincronizacao.Text = banco.GravarUltimaSincronizacao(usuario).ToString("dd/MM/yyyy HH:mm:ss");

                DisplayAlert("Sincronização", "Efetuada com Sucesso!", "OK");
            }
            catch (Exception e)
            {
                DisplayAlert("Falha na Sincronização", e.Message, "OK");
            }

        }
    }
}