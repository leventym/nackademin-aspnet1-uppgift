using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieMonster.Models.Forms
{
    public class MovieCreateForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public List<int> Categories { get; set; }
        public List<int> Cinemas { get; set; }
        public List<int> Actors { get; set; }
    }
}
