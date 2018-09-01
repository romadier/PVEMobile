using PVEM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Sessao
{
    public sealed class Session
    {
        private static volatile Session instance;
        private static object sync = new Object();

        private Session() { }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new Session();
                        }
                    }
                }
                return instance;
            }

        }

        public MobileUsuarioModel UsuarioLogado { get; set; }
    }
}
