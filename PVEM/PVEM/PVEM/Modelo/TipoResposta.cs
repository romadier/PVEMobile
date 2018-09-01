using SQLite;
using System.Collections.Generic;

namespace PVEM.Modelo
{

    public class TipoResposta
    {
        [PrimaryKey]
        public short IdtTipoResposta { get; set; }

        public string DescTipoResposta { get; set; }

        [Ignore]
        public virtual ICollection<OpcaoTipoResposta> Opcoes { get; set; }
    }
}
