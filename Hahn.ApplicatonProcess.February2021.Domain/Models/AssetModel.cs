using System;
using Hahn.ApplicatonProcess.February2021.Data.Enums;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models
{
    public class AssetModel
    {
        public int ID { get; set; }
        public string AssetName { get; set; }
        public DepartmentEnum Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
