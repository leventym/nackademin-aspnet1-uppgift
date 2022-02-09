using MovieMonsterApi.Repositories;

namespace MovieMonsterApi.Entities
{
    public class Actor : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
