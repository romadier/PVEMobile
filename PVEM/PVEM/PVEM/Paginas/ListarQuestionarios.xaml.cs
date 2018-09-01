using PVEM.Banco;
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
    public partial class ListarQuestionarios : ContentPage
    {
        public ListarQuestionarios()
        {
            InitializeComponent();

            AcessoBanco banco = new AcessoBanco();

            List<RespostaQuestionario> questionarios = banco.PegarQuestionariosUsuario(Session.Instance.UsuarioLogado.IdAspNetUser);

            foreach (var item in questionarios)
            {
                Button varbtn = new Button
                {
                    Text = item.NomQuestionario,
                    CommandParameter = item.IdtRespostaQuestionario,

                };
                varbtn.Clicked += IrQuestionario;

                slBotoes.Children.Add(varbtn);
            }
            
        }
        public void IrQuestionario(object sender, EventArgs args)
        {
            DisplayAlert("Questionario", (sender as Button).CommandParameter.ToString(), "OK");
        }
    }
}