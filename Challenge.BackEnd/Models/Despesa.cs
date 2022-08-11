namespace Challenge.BackEnd.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
        public float valor { get; set; }
        public DateTime Data { get; set; }

    }

}
