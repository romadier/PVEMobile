using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PVEM.Servico.Modelo;
using Xamarin.Forms;

namespace PVEM.Banco
{
    class AcessoBanco
    {
        private SQLiteConnection _conexao;

        public AcessoBanco ()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("data3.sqlite");

            _conexao = new SQLiteConnection(caminho);
            _conexao.CreateTable<MobileUsuarioModel>();
        }

        public void IncluirUsuario(MobileUsuarioModel usuario)
        {
            _conexao.Insert(usuario);
        }

        public void AtualizarUsuario(MobileUsuarioModel usuarioModel)
        {
            _conexao.Update(usuarioModel);
        }

        public MobileUsuarioModel ValidarSenhaUsuario(string login, string senha)
        { 
            return _conexao.Table<MobileUsuarioModel>().Where(u => u.Login == login && u.Senha == senha).FirstOrDefault();
        }

        public bool UsuarioJaCadastrado(string idAspNetUser)
        {
            return _conexao.Table<MobileUsuarioModel>().Where(u => u.IdAspNetUser == idAspNetUser).Count() > 0;
        }
    }
}
