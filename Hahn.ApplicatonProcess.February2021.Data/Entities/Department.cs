using System.Collections.Generic;
using Hahn.ApplicatonProcess.February2021.Data.Enums;

namespace Hahn.ApplicatonProcess.February2021.Data.Entities
{
    public class Department
    {
        public Department()
        {
            Assets = new HashSet<Asset>();
        }

        public DepartmentEnum ID { get; set; }
        public string Name { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}