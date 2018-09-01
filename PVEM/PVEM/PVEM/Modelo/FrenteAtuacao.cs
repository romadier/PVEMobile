using SQLite;

namespace PVEM.Modelo
{
    
    public  class FrenteAtuacao
    {
        [PrimaryKey]
        public int IdtFrenteAtuacao { get; set; }

        public string DescFrenteAtuacao { get; set; }

    }
}
