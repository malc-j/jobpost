using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace JobPost.NUnit.Test
{
    internal class MockDbContext : AppDbContext
    {

        public MockDbContext(DbContextOptions options) : base(options)
        {
        }

        //public MockDbContext()
        //{
            
        //}


    }
}
