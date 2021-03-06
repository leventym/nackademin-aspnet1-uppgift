using MovieMonsterApi.Repositories;

namespace MovieMonsterApi.Entities
{
    public class Movie : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public List<Category> Categories { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
