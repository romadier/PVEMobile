using SQLite;

namespace PVEM.Modelo
{
    public class Ciclo
    {
        [PrimaryKey]
        public short IdtCiclo { get; set; }

        public string DescCiclo { get; set; }
    }
}
