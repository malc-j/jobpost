using JobPost.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.MockData
{
    public class Seed
    {
        private AppDbContext _context;

        public Seed(AppDbContext context)
        {
            _context = context;
        }

        public async Task GenerateSeed()
        {
            var employers = await _context.Employers.ToListAsync();

            if(employers.Count() == 0)
            {
                List<Employer> mockEmployers = new List<Employer>();
                _context.Employers.Add(new Employer("Margarethe", "Pankettman", "193-930-1242", "Foundation Building Materials, Inc.", "mpankettman0@digg.com"));
                _context.Employers.Add(new Employer("Mildrid", "Billington", "995-624-1154", "VCA Inc.", "mbillington1@archive.org"));
                _context.Employers.Add(new Employer("Ardelis", "Wickrath", "947-408-8129", "LKQ Corporation", "awickrath2@squarespace.com"));

                await _context.SaveChangesAsync();

                //mockEmployers.Add(new Employer("Margarethe", "Pankettman", "193-930-1242", "Foundation Building Materials, Inc.", "mpankettman0@digg.com"));
                //mockEmployers.Add(new Employer("Mildrid", "Billington", "995-624-1154", "VCA Inc.", "mbillington1@archive.org"));
                //mockEmployers.Add(new Employer("Ardelis", "Wickrath", "947-408-8129", "LKQ Corporation", "awickrath2@squarespace.com"));

            }

            employers = await _context.Employers.ToListAsync();

            if(employers.Count() != 0)
            {

                _context.Posts.Add(new Post(employers[0],"Database Administrator III", "Other nondisplaced fracture of base of first metacarpal bone, right hand, subsequent encounter for fracture with delayed healing", "Mexico", "La Victoria"));
                _context.Posts.Add(new Post(employers[0], "Information Systems Manager", "Unspecified injury of unspecified muscle, fascia and tendon at wrist and hand level, right hand, initial encounter", "Syria", "Arwād"));
                _context.Posts.Add(new Post(employers[0], "Administrative Assistant IV", "Unspecified injury of unspecified muscle, fascia and tendon at wrist and hand level, right hand, initial encounter", "United States", "St Luis"));

                _context.Posts.Add(new Post(employers[1], "Computer Systems Analyst III", "Other nondisplaced fracture of base of first metacarpal bone, right hand, subsequent encounter for fracture with delayed healing", "Sweden", "Gothenburg"));
                _context.Posts.Add(new Post(employers[1], "Environmental Tech", "Unspecified injury of unspecified muscle, fascia and tendon at wrist and hand level, right hand, initial encounter", "Germany", "Berlin"));
                _context.Posts.Add(new Post(employers[1], "Mechanical Systems Engineer", "Unspecified injury of unspecified muscle, fascia and tendon at wrist and hand level, right hand, initial encounter", "Norway", "Oslo"));
                await _context.SaveChangesAsync();
            }
        }


    }
}
