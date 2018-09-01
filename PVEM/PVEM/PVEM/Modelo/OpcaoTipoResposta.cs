using SQLite;
using System;

namespace PVEM.Modelo
{
    public class OpcaoTipoResposta
    {
        [PrimaryKey]
        public int IdtOpcaoTipoResposta { get; set; }

        public short IdtTipoResposta { get; set; }

        public string DescOpcao { get; set; }

        public double? Peso { get; set; }
    }
}