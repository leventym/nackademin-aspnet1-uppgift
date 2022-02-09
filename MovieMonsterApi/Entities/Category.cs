using MovieMonsterApi.Repositories;

namespace MovieMonsterApi.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
