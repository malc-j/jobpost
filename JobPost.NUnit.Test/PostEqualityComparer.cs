using JobPost.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPost.NUnit.Test
{
    public class PostEqualityComparer: IEqualityComparer<Post>
    {
        public bool Equals(Post? x, Post? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id
                && x.EmployerId == y.EmployerId
                && x.Title == y.Title
                && x.Description == y.Description
                && x.City == y.City
                && x.EndDate == y.EndDate
                && x.CreatedAt == y.CreatedAt;

            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Post obj)
        {
            return HashCode.Combine(obj.Id, obj.EmployerId, obj.Title, obj.Description, obj.City, obj.EndDate, obj.CreatedAt);
            throw new NotImplementedException();
        }
    }
}
