using PVEM.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;

namespace PVEM.Servico
{
    public class UsuarioServico
    {
        private static readonly string UrlBase = "http://localhost:56110/Mobile/{0}";

        public static MobileUsuarioModel BuscarUsuario(string login, string senha)
        {
            string urlBuscarUsuario = String.Format(UrlBase, "GetUsuario");

            MobileLoginModel loginModel = new MobileLoginModel()
            {
                Login = login,
                Senha = senha
            };

            string loginJson = JsonConvert.SerializeObject(loginModel);

            var cli = new WebClient();
            cli.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response =  cli.UploadString(urlBuscarUsuario, loginJson);

            MobileUsuarioModel usuarioModel = new MobileUsuarioModel();

            /*if (response != "null")
                usuarioModel = JsonConvert.DeserializeObject<MobileUsuarioModel>(response);*/

            return usuarioModel;
        }
    }
}
