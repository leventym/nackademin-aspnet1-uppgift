namespace MovieMonsterApi.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
