using MovieMonsterApi.Entities;

namespace MovieMonsterApi.Models
{
    public class ActorCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> Movies { get; set; }
    }
}
