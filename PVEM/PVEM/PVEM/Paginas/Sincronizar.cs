using Newtonsoft.Json;
using PVEM.Banco;
using PVEM.Modelo;
using PVEM.Servico;
using PVEM.Sessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PVEM.Paginas
{
    public class Sincronizar : ContentPage
    {
        private Label lblUltimaSincronizacao;
        private ActivityIndicator activityIndicator;

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

            activityIndicator = new ActivityIndicator { IsRunning = true, IsVisible = false, Color = Color.Black };

            Content = new StackLayout
            {
                Children = {
                    new Image {
                        Source ="logo.png",
                        BackgroundColor = Color.FromHex("#3A3732"),
                        HorizontalOptions = LayoutOptions.Fill,
                        HeightRequest = 75
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
                            activityIndicator,
                            btnSincronizar
                        }
                    }
                }
            };

            btnSincronizar.Clicked +=  AcaoSincronizar;

        }
        
        public async void AcaoSincronizar(object sender, EventArgs args)
        {
            try
            {
                try
                {
                    (sender as Button).IsEnabled = false;
                    activityIndicator.IsVisible = true;
                    await Task.Delay(500);

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

                    List<Municipio> municipios = UsuarioServico.BaixarMunicipios();

                    foreach (var item in municipios)
                    {
                        banco.GravarMunicipio(item);
                    }

                    List<AlternativaICQ> alteranativas = UsuarioServico.BaixarAlternativas();

                    foreach (var item in alteranativas)
                    {
                        banco.GravarAlternativa(item);
                    }

                    List<OpcaoTipoResposta> opcoes = UsuarioServico.BaixarOpcoes();

                    foreach (var item in opcoes)
                    {
                        banco.GravarOpcao(item);
                    }

                    List<QuestionarioRespondido> pendentes = banco.ListarRespostasNaoEnvidas();

                    foreach (var item in pendentes)
                    {
                        RespostaQuestionarioForm formTmp = JsonConvert.DeserializeObject<RespostaQuestionarioForm>(item.Formulario);
                        if (UsuarioServico.TransmitirResposta(formTmp))
                        {
                            banco.MarcarRespostaComoEnviada(item.Id);
                        }
                    }

                    lblUltimaSincronizacao.Text = banco.GravarUltimaSincronizacao(usuario).ToString("dd/MM/yyyy HH:mm:ss");

                    activityIndicator.IsVisible = false;
                    await DisplayAlert("Sincronização", "Efetuada com Sucesso!", "OK");

                }
                catch (Exception e)
                {
                    activityIndicator.IsVisible = false;
                    await DisplayAlert("Falha na Sincronização", e.Message, "OK");
                }
            }
            finally
            {
                (sender as Button).IsEnabled = true;
            }
        }
    }
}