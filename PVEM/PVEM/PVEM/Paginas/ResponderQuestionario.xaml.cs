using Newtonsoft.Json;
using Plugin.InputKit.Shared.Controls;
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
using Xamarin.Forms.Xaml;

namespace PVEM.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResponderQuestionario : ContentPage
	{
        private Dictionary<int, RadioButtonGroupView> _alternativas;
        private Dictionary<int, RadioButtonGroupView> _opcoes;
        private long _IdtRespostaQuestionario;
        private RespostaQuestionarioForm _form;


        public ResponderQuestionario (long id)
		{
			InitializeComponent();

            _alternativas = new Dictionary<int, RadioButtonGroupView>();
            _opcoes = new Dictionary<int, RadioButtonGroupView>();
            _IdtRespostaQuestionario = id;

            AcessoBanco banco = new AcessoBanco();

            RespostaQuestionario questionario = banco.PegarQuestionario(id);

            _form = JsonConvert.DeserializeObject<RespostaQuestionarioForm>(questionario.Objeto);

            List <Municipio> municipios = banco.ListarMunicipios();

            List<AlternativaICQ> alternativas = banco.ListarAlternativas();

            List<OpcaoTipoResposta> opcoes = banco.ListarOpcoes();

            NomeRelatorio.Text = _form.NomeRelatorio;
            TextoCabecalho.Text = _form.TextoCabecalho;
            
            foreach (var item in municipios)
            {
                Municipio.Items.Add(item.NomMunicipio);
            }

            foreach (var item in _form.RespostasPerfil)
            {
                StackLayoutPrincipal.Children.Add(new Label() {
                    Text = item.DescPergunta
                });

                var rbgAlternativa = new RadioButtonGroupView();

                foreach (var alternativa in alternativas.Where(a => a.IdtItemCabecalhoQuestionario == item.IdtItemCabecalhoQuestionario).ToList())
                {
                    rbgAlternativa.Children.Add(new RadioButton()
                    {
                        Value = alternativa.IdtAlternativaICQ,
                        Text = alternativa.DescAlternativa,
                        TextFontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                    });        
                }

                StackLayoutPrincipal.Children.Add(rbgAlternativa);
                _alternativas.Add(item.IdtItemCabecalhoQuestionario, rbgAlternativa);
            }

            foreach (var item in _form.Competencias)
            {
                StackLayoutPrincipal.Children.Add(new Label()
                {
                    Text = item.DescCompetencia,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                });
                AdicionarLinha();

                foreach (var resposta in item.Respostas)
                {
                    StackLayoutPrincipal.Children.Add(new Label()
                    {
                        Text = resposta.DescPergunta
                    });

                    var rbgOpcao = new RadioButtonGroupView();

                    foreach (var opcao in opcoes.Where(o => o.IdtTipoResposta == resposta.IdtTipoResposta).ToList())
                    {
                        rbgOpcao.Children.Add(new RadioButton()
                        {
                            Value = opcao.IdtOpcaoTipoResposta,
                            Text = opcao.DescOpcao,
                            TextFontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                        });
                    }
                    StackLayoutPrincipal.Children.Add(rbgOpcao);
                    _opcoes.Add(resposta.IdtQuestionarioPergunta, rbgOpcao);
                }

            }

            
            Button btnConfirmar = new Button()
            {
                Text = "Salvar",
                HorizontalOptions = LayoutOptions.Center,
                Margin = 20
            };

            btnConfirmar.Clicked += Salvar;

            StackLayoutPrincipal.Children.Add(btnConfirmar);

        }

        private void AdicionarLinha()
        {
            StackLayoutPrincipal.Children.Add(new BoxView()
            {
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand
            });
        }

        private void Salvar(object sender, EventArgs args)
        {
            if (Municipio.SelectedIndex == -1)
            {
                DisplayAlert("Questionário", "Selecione o Município!", "OK");
                return;
            }

            foreach (var item in _opcoes)
            {
                if (item.Value.SelectedIndex == -1)
                {
                    DisplayAlert("Questionário", "Uma ou mais perguntas estão sem resposta selecionada!", "OK");
                    return;
                }
            }

            foreach (var item in _alternativas)
            {
                if (item.Value.SelectedIndex == -1)
                {
                    DisplayAlert("Questionário", "Uma ou mais perguntas estão sem resposta selecionada!", "OK");
                    return;
                }
            }

            AcessoBanco banco = new AcessoBanco();

            Municipio municipio = banco.PegarMunicipioPorNome((string) Municipio.SelectedItem);

            _form.Municipio = municipio;

            foreach (var item in _form.RespostasPerfil)
            {
                item.IdtAlternativaICQ = (int) _alternativas[item.IdtItemCabecalhoQuestionario].SelectedItem ;
            }

            foreach (var comp in _form.Competencias)
            {
                foreach (var item in comp.Respostas)
                {
                    item.IdtOpcaoTipoResposta = (int) _opcoes[item.IdtQuestionarioPergunta].SelectedItem;
                }
            }

            banco.GravarResposta(_form, Session.Instance.UsuarioLogado.IdAspNetUser);

            try
            {
                List<QuestionarioRespondido> pendentes = banco.ListarRespostasNaoEnvidas();

                foreach (var item in pendentes)
                {
                    RespostaQuestionarioForm formTmp = JsonConvert.DeserializeObject<RespostaQuestionarioForm>(item.Formulario);
                    if (UsuarioServico.TransmitirResposta(formTmp))
                    {
                        banco.MarcarRespostaComoEnviada(item.Id);
                    }
                }
                
            }
            catch (Exception)
            {

                
            }

            DisplayAlert("Questionário", "Incluído com Sucesso", "OK");
            Navigation.PopAsync();


        }

    }
}