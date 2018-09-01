using SQLite;
using System;

namespace PVEM.Modelo
{

    public partial class Questionario
    {
        [PrimaryKey]
        public int IdtQuestionario { get; set; }

        public short Ano { get; set; } 

        public short IdtCiclo { get; set; }

        public int IdtFrenteAtuacao { get; set; }

        public string NomQuestionario { get; set; }

        public int IdtFrenteAtuacaoAvalia { get; set; }

        public DateTime DataHoraGeracao { get; set; }

        public string Usuario { get; set; }
    }
}
