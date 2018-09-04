using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Modelo
{
    public class AlternativaICQ
    {
        [PrimaryKey]
        public int IdtAlternativaICQ { get; set; }

        public int IdtItemCabecalhoQuestionario { get; set; }

        public string DescAlternativa { get; set; }

        [Ignore]
        public bool PodeExcluir { get; set; }

        [Ignore]
        public bool Excluido { get; set; }
    }
}
