using SQLite;

namespace PVEM.Modelo
{
    class MobileLoginModel
    {
        [PrimaryKey]
        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
