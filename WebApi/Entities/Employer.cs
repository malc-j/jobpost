using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobPost.Models
{
    public class Employer
    {
        [Key]
        public string Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
