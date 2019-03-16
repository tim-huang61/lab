using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Lab.Entities
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }

        public string Phone { get; set; }
    }

    public class JoeyEmployeeWithPhoneEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                   && x.LastName == y.LastName
                   && x.Phone == y.Phone;
        }

        // 有人使用hash code做加總但不好，加總可能會一樣
        // 透過匿名型別取得get hash code
        // Tuple get hash code
        public int GetHashCode(Employee obj)
        {
            throw new NotImplementedException();
            // return obj.FirstName.GetHashCode() + obj.LastName.GetHashCode();
            // return new {obj.FirstName, obj.LastName}.GetHashCode();
            // return Tuple.Create(obj.FirstName, obj.LastName).GetHashCode();
        }
    }
}