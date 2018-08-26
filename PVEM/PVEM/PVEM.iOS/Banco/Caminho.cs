using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PVEM.Banco;
using UIKit;
using Xamarin.Forms;
using System.IO;
using PVEM.iOS.Banco;

[assembly: Dependency(typeof(Caminho))]
namespace PVEM.iOS.Banco
{
    class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            string caminhoPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string CaminhoBiblioteca = Path.Combine(caminhoPasta, "..", "Library");

            string caminhoBanco = Path.Combine(CaminhoBiblioteca, NomeArquivoBanco);

            return caminhoBanco;
        }
    }
}