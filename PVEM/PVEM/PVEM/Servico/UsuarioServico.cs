using PVEM.Servico.Modelo;
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
        private static readonly string UrlBase = "http://192.168.43.198:56110/Mobile/{0}";

        public static MobileUsuarioModel BuscarUsuario(string login, string senha)
        {
            string urlBuscarUsuario = String.Format(UrlBase, "GetUsuario");


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
    }
}
