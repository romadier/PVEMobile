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
        public async void IrQuestionario(object sender, EventArgs args)
        {
            try
            {
                (sender as Button).IsEnabled = false;
                await Task.Delay(500);
                await Navigation.PushAsync(new Paginas.ResponderQuestionario(Int64.Parse((sender as Button).CommandParameter.ToString())));
            }
            finally
            {
                (sender as Button).IsEnabled = true;
            }
        }
    }
}