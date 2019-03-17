using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class JoeyEmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            return new {obj.LastName, obj.FirstName}.GetHashCode();
        }
    }
}