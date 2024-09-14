using System.ComponentModel.DataAnnotations;

namespace JobPost.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        public Guid EmployerId { get; set; }

        public Employer Employer { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        private string Country { get; set; }

        public string City { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Post()
        {
            
        }

        public Post(Employer Employer, string Title, string Description, string Country, string City)
        {
            this.Employer = Employer;
            this.Title = Title;
            this.Description = Description;
            this.Country = Country;
            this.City = City;
        }

    }
}
