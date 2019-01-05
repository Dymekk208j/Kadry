using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kadry.Models
{
    public class Employeer
    {
        private static readonly SQLConnection SqlConnection = new SQLConnection();

        public int Id { get; set; }

        [Display(Name="Nazwisko: ")]
        public string Surname { get; set; }

        [Display(Name = "Imię: ")]
        public string Firstname { get; set; }

        [Display(Name = "Drugie imię: ")]
        public string MiddleName { get; set; }

        [Display(Name = "PESEL: ")]
        public string Pesel { get; set; }

        [Display(Name = "Data urodzenia: ")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Płeć: ")]
        public string Sex { get; set; }

        public Login Login { get; set; }
        public Workplace Workplace { get; set; }
        public Holiday Holiday { get; set; }
        public ContractType ContractType { get; set; }
        public Medical Medical { get; set; }
        public Salary Salary { get; set; }
        public Hours Hours { get; set; }

        [Display(Name = "Data zawarcia umowy: ")]
        public DateTime ContractDate { get; set; }

        [Display(Name = "Data zakończenia umowy: ")]
        public DateTime ContractEndDate { get; set; }

        public static List<ContractType> GetContractTypeList()
        {
            return SqlConnection.ContractTypeList();
        }

        public static List<Workplace> GetWorkplaceList()
        {
            return SqlConnection.WorkplaceList();
        }
    }
}