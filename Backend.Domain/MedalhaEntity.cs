namespace Backend.Domain
{
    public class MedalhaEntity : BaseEntity
    {
        public string Pais { get; set; } = string.Empty;
        public int Ouro { get; set; }
        public int Prata { get; set; }
        public int Bronze { get; set; }
        public int Total { get; set; }
    }
}