using System.ComponentModel.DataAnnotations;

namespace MovieMonster.Models
{
    public class Movie
    {
        public Movie()
        {

        }
        public Movie(int id, string name, string description, int price, int startDate, int endDate, List<Category> categories, List<Cinema> cinemas, List<Actor> actors)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StartDate = startDate;
            EndDate = endDate;
            Categories = categories;
            Cinemas = cinemas;
            Actors = actors;
        }

        [Key]
        public int Id { get; set; }
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
