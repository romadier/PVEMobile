using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Modelo
{
    public class Sincronizacao
    {
        [PrimaryKey]
        public string IdAspNetUser { get; set; }

        public DateTime DataHora { get; set; }
    }
}
