using System.ComponentModel.DataAnnotations;

namespace JobPost.Models
{
    public class Post
    {
		[Key]
		public Guid Id { get; set; }

        public int EmployerId { get; set; }

        public Employer Employer { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        private string Country { get; set; }

        public string City { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
