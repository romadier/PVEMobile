using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PVEM.Modelo;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace PVEM.Banco
{
    class AcessoBanco
    {
        private SQLiteConnection _conexao;

        public AcessoBanco()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("data3.sqlite");

            _conexao = new SQLiteConnection(caminho);
            _conexao.CreateTable<MobileUsuarioModel>();
            _conexao.CreateTable<QuestionariosUsuario>();
            _conexao.CreateTable<Sincronizacao>();
            _conexao.CreateTable<RespostaQuestionario>();
                       
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

        public DateTime? UltimaSincronizacao(string idAspNetUser)
        {
            Sincronizacao sincronizacao = _conexao.Table<Sincronizacao>().Where(s => s.IdAspNetUser == idAspNetUser).FirstOrDefault();

            DateTime? retorno = null;

            if (sincronizacao != null)
            {
                retorno = sincronizacao.DataHora;
            }

            return retorno;
        }

        public DateTime GravarUltimaSincronizacao(string idAspNetUser)
        {
            Sincronizacao sincronizacao = _conexao.Table<Sincronizacao>().Where(s => s.IdAspNetUser == idAspNetUser).FirstOrDefault();

            DateTime novaDataHora = DateTime.Now;

            if (sincronizacao == null)
            {
                sincronizacao = new Sincronizacao()
                {
                    IdAspNetUser = idAspNetUser,
                    DataHora = novaDataHora
                };
                _conexao.Insert(sincronizacao);
            }
            else
            {
                sincronizacao.DataHora = novaDataHora;
                _conexao.Update(sincronizacao);
            }
            return novaDataHora;
        }

        public void GravarQuestionariosUsuario(string idAspNetUser, List<long> questionarios)
        {

            _conexao.Table<QuestionariosUsuario>().Delete(q => q.IdAspNetUser == idAspNetUser);

            foreach (var item in questionarios)
            {
                _conexao.Insert(new QuestionariosUsuario()
                {
                    IdAspNetUser = idAspNetUser,
                    IdtRespostaQuestionario = item
                });
            }
        }

        public bool ExisteQuestionario(long id)
        {
            return _conexao.Table<RespostaQuestionario>().Where(q => q.IdtRespostaQuestionario == id).Count() > 0;
        }

        public void GravarQuestionario(RespostaQuestionarioForm questionarioForm)
        {
            RespostaQuestionario questionario = new RespostaQuestionario();

            questionario.IdtRespostaQuestionario = questionarioForm.IdtRespostaQuestionario;

            questionario.NomQuestionario = questionarioForm.NomeRelatorio;

            questionario.Objeto = JsonConvert.SerializeObject(questionarioForm);

            _conexao.Insert(questionario);
        }

        public List<RespostaQuestionario> PegarQuestionariosUsuario(string idAspNetUser)
        {

            SQLiteCommand command = _conexao.CreateCommand("Select * from RespostaQuestionario");

            List<RespostaQuestionario> resultado = command.ExecuteQuery<RespostaQuestionario>();

            return resultado;
        }

    }   
}
