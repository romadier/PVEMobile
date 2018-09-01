using SQLite;

namespace PVEM.Modelo
{
    [Table("Usuario")]
    public class MobileUsuarioModel
    {
        [PrimaryKey]
        public string IdAspNetUser { get; set; }

        public string Login { get; set; }

        public string Nome { get; set; }

        public string Perfil { get; set; }

        public string Senha { get; set; }
    }
}
