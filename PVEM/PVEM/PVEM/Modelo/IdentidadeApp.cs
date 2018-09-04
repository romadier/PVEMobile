using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Modelo
{
    public class IdentidadeApp
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string IdDevice { get; set; }
    }
}
