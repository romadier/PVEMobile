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
            string caminho = dep.ObterCaminho("data1.db");

            _conexao = new SQLiteConnection(caminho);
            _conexao.CreateTable<MobileUsuarioModel>();
            _conexao.CreateTable<QuestionariosUsuario>();
            _conexao.CreateTable<Sincronizacao>();
            _conexao.CreateTable<RespostaQuestionario>();
            _conexao.CreateTable<Municipio>();
            _conexao.CreateTable<OpcaoTipoResposta>();
            _conexao.CreateTable<AlternativaICQ>();
            _conexao.CreateTable<QuestionarioRespondido>();
            _conexao.CreateTable<IdentidadeApp>();
            
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

            SQLiteCommand command = _conexao.CreateCommand(String.Format( "Select * from QuestionariosUsuario where idAspNetUser == '{0}'",idAspNetUser));

            List<QuestionariosUsuario> questionarios = command.ExecuteQuery<QuestionariosUsuario>();

            List<RespostaQuestionario> resultado = new List<RespostaQuestionario>();

            foreach (var item in questionarios)
            {
                resultado.Add(_conexao.Table<RespostaQuestionario>().Where(r => r.IdtRespostaQuestionario == item.IdtRespostaQuestionario).FirstOrDefault());
            }

            return resultado;
        }

        public RespostaQuestionario PegarQuestionario(long id)
        {
            return _conexao.Table<RespostaQuestionario>().Where(r => r.IdtRespostaQuestionario == id).FirstOrDefault();
        }

        public void GravarMunicipio(Municipio municipio)
        {
            if (_conexao.Table<Municipio>().Where(m => m.IdtMunicipio == municipio.IdtMunicipio).Count() == 0)
            {
                _conexao.Insert(municipio);
            }
            else
            {
                _conexao.Update(municipio);
            }
        }

        public void GravarOpcao(OpcaoTipoResposta opcao)
        {
            if (_conexao.Table<OpcaoTipoResposta>().Where(m => m.IdtOpcaoTipoResposta == opcao.IdtOpcaoTipoResposta).Count() == 0)
            {
                _conexao.Insert(opcao);
            }
            else
            {
                _conexao.Update(opcao);
            }
        }

        public void GravarAlternativa(AlternativaICQ alternativa)
        {
            if (_conexao.Table<AlternativaICQ>().Where(m => m.IdtAlternativaICQ == alternativa.IdtAlternativaICQ).Count() == 0)
            {
                _conexao.Insert(alternativa);
            }
            else
            {
                _conexao.Update(alternativa);
            }
        }

        public List<Municipio> ListarMunicipios()
        {
            SQLiteCommand command = _conexao.CreateCommand("Select * from Municipio ");
            List<Municipio> municipios = command.ExecuteQuery<Municipio>();
            return municipios;
        }

        public List<AlternativaICQ> ListarAlternativas()
        {
            SQLiteCommand command = _conexao.CreateCommand(" Select * from AlternativaICQ ");
            List<AlternativaICQ> resultado = command.ExecuteQuery<AlternativaICQ>();

            return resultado;
        }

        public List<OpcaoTipoResposta> ListarOpcoes()
        {
            SQLiteCommand command = _conexao.CreateCommand(" Select * from OpcaoTipoResposta ");
            List<OpcaoTipoResposta> resultado = command.ExecuteQuery<OpcaoTipoResposta>();

            return resultado;
        }

        public void GravarResposta(RespostaQuestionarioForm resposta, string usuario)
        {
            IdentidadeApp app = _conexao.Table<IdentidadeApp>().FirstOrDefault();

            string idDevice;

            if (app == null)
            {
                idDevice = Guid.NewGuid().ToString("d");
            }
            else
            {
                idDevice = app.IdDevice;
            }

            resposta.IdDevice = idDevice;
            DateTime dataHora = DateTime.Now;
            resposta.DataHoraDevice = dataHora;
            resposta.Usuario = usuario;

            _conexao.Insert( new QuestionarioRespondido()
            {
                Formulario = JsonConvert.SerializeObject(resposta),
                DataHoraResposta = dataHora,
                Usuario = usuario,
                IdDevice = idDevice
            });
        }

        public List<QuestionarioRespondido>  ListarRespostasNaoEnvidas()
        {
            SQLiteCommand command = _conexao.CreateCommand(" Select * from QuestionarioRespondido where DataHoraEnvio is Null ");
            List<QuestionarioRespondido> resultado = command.ExecuteQuery<QuestionarioRespondido>();

            return resultado;
        }

        public void MarcarRespostaComoEnviada(long id)
        {
            QuestionarioRespondido respondido = _conexao.Table<QuestionarioRespondido>().Where(r => r.Id == id).FirstOrDefault();

            respondido.DataHoraEnvio = DateTime.Now;

            _conexao.Update(respondido);
        }

        public Municipio PegarMunicipioPorNome(string nome)
        {
            return _conexao.Table<Municipio>().Where(m => m.NomMunicipio == nome).FirstOrDefault();
        }

    }
}

