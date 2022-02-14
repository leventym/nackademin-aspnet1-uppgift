using System.ComponentModel.DataAnnotations;

namespace MovieMonster.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
