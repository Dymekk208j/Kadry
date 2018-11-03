using System;

namespace Kadry.Models
{
    public class Employeer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string Pesel { get; set; }
        public DateTime Birthday { get; set; }
        public bool Sex { get; set; }
        public Login Login { get; set; }
        public Workplace Workplace { get; set; }
        public Holiday Holiday { get; set; }
        public ContractType ContractType { get; set; }
        public Medical Medical { get; set; }
        public Salary Salary { get; set; }
        public Hours Hours { get; set; }
    }
}