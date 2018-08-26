using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Banco
{
    public interface ICaminho
    {
        string ObterCaminho(string NomeArquivoBanco);
    }
}
