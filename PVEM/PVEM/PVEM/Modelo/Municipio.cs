using SQLite;

namespace PVEM.Modelo
{
    public class Municipio
    {
        [PrimaryKey]
        public int IdtMunicipio { get; set; }

        public string NomMunicipio { get; set; }

        public int? CodIBGE { get; set; }

        public int IdtEmpresa { get; set; }

        public string UF { get; set; }
    }
}