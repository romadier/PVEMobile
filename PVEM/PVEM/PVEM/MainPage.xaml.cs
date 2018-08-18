using PVEM.Servico;
using PVEM.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PVEM
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {   
            InitializeComponent();
            Ir.Clicked += BuscarUsuario;
        }

        public void BuscarUsuario(object sender, EventArgs args)
        {
            string login = Login.Text;
            string senha = Senha.Text;

            MobileUsuarioModel usuario = UsuarioServico.BuscarUsuario(login, senha);

            if (usuario != null)
                Retorno.Text = usuario.Nome;
            else
                Retorno.Text = "Usuário ou Senha Inválida!";
        }
    }
}
