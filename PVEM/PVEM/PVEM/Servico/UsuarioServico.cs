using PVEM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PVEM.Servico
{
    public class UsuarioServico
    {
        private static readonly string UrlBase = "http://192.168.0.22:56110/{0}";

        public static MobileUsuarioModel BuscarUsuario(string login, string senha)
        {
            string urlBuscarUsuario = String.Format(UrlBase, "Mobile/GetUsuario");


            FormUrlEncodedContent param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string> ("Login",login),
                new KeyValuePair<string,string> ("Senha",senha)
            });
         
            MobileUsuarioModel usuarioModel = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta =  requisicao.PostAsync(urlBuscarUsuario, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                usuarioModel = JsonConvert.DeserializeObject<MobileUsuarioModel>(resposta.Content.ReadAsStringAsync().Result);
            }

            return usuarioModel;
        }

        public static List<long> PegarQuestionariosUsuario(string IdAspNetUser)
        {
            string urlPegarQuestionariosUsuario = String.Format(UrlBase, "Mobile/QuestionariosPorUsuario");

            List<long> resultado =  new List<long>();

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string> ("Id",IdAspNetUser)
            });


            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(urlPegarQuestionariosUsuario, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                resultado = JsonConvert.DeserializeObject<List<long>>(resposta.Content.ReadAsStringAsync().Result);
            }
            return resultado;
        }

        public static RespostaQuestionarioForm BaixarQuestionario(long id)
        {
            string urlAux = String.Format(UrlBase, "/Questionario/Preencher?Id=" + id.ToString());

            RespostaQuestionarioForm resultado = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(urlAux).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                resultado = JsonConvert.DeserializeObject<RespostaQuestionarioForm>(resposta.Content.ReadAsStringAsync().Result);
            }

            return resultado;
        }


    }
}
