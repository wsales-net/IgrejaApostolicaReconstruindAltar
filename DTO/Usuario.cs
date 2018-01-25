namespace DTO
{
    public class Usuario
    {
        public int Id { get; set; }
        public Membro Pessoa { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario() { }

    }
}
