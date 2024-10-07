using JobPost.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPost.NUnit.Test
{
    public class EmployerEqualityComparer : IEqualityComparer<Employer>
    {
        public bool Equals(Employer? x, Employer? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.Firstname == y.Firstname
                && x.Lastname == y.Lastname
                && x.CompanyName == y.CompanyName
                && x.Phone == y.Phone
                && x.CreatedAt == y.CreatedAt
                && x.Email == y.Email;

            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Employer obj)
        {
            return HashCode.Combine(obj.Id, obj.Firstname, obj.Lastname, obj.Email, obj.CompanyName, obj.Phone, obj.CompanyName, obj.CreatedAt);
            throw new NotImplementedException();
        }
    }
}
