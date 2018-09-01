namespace PVEM.Modelo
{
    using System;
    using System.Collections.Generic;

    public partial class Competencia
    {
        public int IdtCompetencia { get; set; }

        public string DescCompetencia { get; set; }

        public int? Ordem { get; set; }
    }
}
