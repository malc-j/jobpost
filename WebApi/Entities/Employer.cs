using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobPost.Models
{
    public class Employer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Employer()
        {
            
        }
        public Employer(string Firstname, string Lastname, string Phone, string CompanyName, string Email)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Phone = Phone;
            this.CompanyName = CompanyName;
            this.Email = Email;               
        }

    }
}
