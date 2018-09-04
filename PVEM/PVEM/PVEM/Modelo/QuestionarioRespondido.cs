using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVEM.Modelo
{
    public class QuestionarioRespondido
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Formulario { get; set; }

        public DateTime DataHoraResposta { get; set; }

        public string Usuario { get; set; }

        public DateTime? DataHoraEnvio { get; set; }

        public string IdDevice { get; set; }
    }
}
