using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Kadry.Models
{
    public class SQLConnection
    {
        // private string connectionString = "Data Source=DESKTOP-VR8CI70;Initial Catalog=PortfolioWebAppV2;Integrated Security=True;Pooling=False";
        //string connectionString = @"Server=.\DESKTOP-VR8CI70;Database=Kadry;Integrated Security=True;";
        private readonly string connectionString =
            "Data Source=DESKTOP-BKQHCMV;Initial Catalog=Kadry;Integrated Security=True;Pooling=False";

        private readonly SqlConnection _connection; // wartosc tylko do odczytu w zakresie klasy

        public SQLConnection()
        {
            _connection = new SqlConnection(connectionString); // Konstruktor to jedyne miejsce gdzie mozemy nadac wartosc polu z właściwością "readonly" (chyba)
            _connection.Open();

        }

        ~SQLConnection()
        {
            _connection.Close();
        }

        private SqlCommand Execute(string query) //wykonuje query , aby nie było trzeba dodawać ścieżki 
        {
            return new SqlCommand(query, _connection); //_c oznacza, że zmienna jest prywatna
        }

        public List<ContractType> ContractTypeList()
        {
            List<ContractType> list = new List<ContractType>();

            string query = "SELECT * FROM Contract_type";
            SqlDataReader reader = Execute(query).ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    var contractType = new ContractType()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Contract_name"].ToString()
                    };
                    list.Add(contractType);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return list;
        }

        public List<Workplace> WorkplaceList()
        {
            List<Workplace> list = new List<Workplace>();

            string query = "SELECT * FROM Workplace";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    var workplace = new Workplace()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Workplace"].ToString()
                    };
                    list.Add(workplace);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return list;
        }

        public List<Employeer> GetAllEmployeers()
        {

            List<Employeer> listEmployeers = new List<Employeer>();
            string query = "SELECT * FROM Employeer";
           
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {

                while (reader.Read())
                {
                   var employer = new Employeer()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Firstname = reader["First_name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        MiddleName = reader["Middle_name"].ToString(),
                        Pesel = reader["Pesel"].ToString(),
                        Birthday = DateTime.Parse(reader["Birth_date"].ToString()),
                        ContractDate = DateTime.Parse(reader["Contract_date"].ToString()),
                        ContractEndDate = DateTime.Parse(reader["Contract_end_date"].ToString()),
                        Sex = reader["Sex"].ToString()
                   };
                    listEmployeers.Add(employer);
                }
            }
            finally
            {
                if (!reader.IsClosed) reader.Close();
            }

            return listEmployeers;
        }

        public Employeer GetEmployer(int id)
        {
            Employeer employer = new Employeer();
            string query = "SELECT * FROM Employeer WHERE id = " + id.ToString();
            //SELECT * FROM Employeer WHERE id=1
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    employer = new Employeer()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Firstname = reader["First_name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        MiddleName = reader["Middle_name"].ToString(),
                        Pesel = reader["Pesel"].ToString(),
                        Birthday = DateTime.Parse(reader["Birth_date"].ToString()),
                        ContractDate = DateTime.Parse(reader["Contract_date"].ToString()),
                        ContractEndDate = DateTime.Parse(reader["Contract_end_date"].ToString()),
                        Sex = reader["Sex"].ToString()
                    };

                    var idContractType = int.Parse(reader["id_Contract_type"].ToString());
                    var idWorkplace = int.Parse(reader["id_Workplace"].ToString());
                    var idHoliday = int.Parse(reader["id_Holiday"].ToString());
                    var idMedical = int.Parse(reader["id_Medical"].ToString());
                    var idSalary = int.Parse(reader["id_Salary"].ToString());
                    var idHours = int.Parse(reader["id_Hours"].ToString());

                    reader.Close();

                    employer.ContractType = GetContractType(idContractType);
                    employer.Workplace = GetWorkplace(idWorkplace);
                    employer.Holiday = GetHoliday(idHoliday);
                    employer.Medical = GetMedical(idMedical);
                    employer.Salary = GetSalary(idSalary);
                    employer.Hours = GetHours(idHours);

                }

            }
            finally
            {
                if(!reader.IsClosed) reader.Close();
            }

            return employer;
        }

        public ContractType GetContractType(int id)
        {
            ContractType contract = new ContractType();
            string query = "SELECT * FROM Contract_type WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    contract = new ContractType()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["Contract_name"].ToString()
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return contract;

        }

        public Workplace GetWorkplace(int id)
        {
            Workplace workplace = new Workplace();
            string query = "SELECT * FROM Workplace WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    workplace = new Workplace()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["Workplace"].ToString()
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return workplace;

        }

        public Holiday GetHoliday(int id)
        {
            Holiday holiday = new Holiday();
            string query = "SELECT * FROM Holiday WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    holiday = new Holiday()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        HolidayAnuses = int.Parse(reader["Holiday_anuses"].ToString()),
                        RemainingHoliday = int.Parse(reader["Remaining_holiday"].ToString()),
                        Year = int.Parse(reader["Year"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return holiday;

        }

        public Medical GetMedical(int id)
        {
            Medical medical = new Medical();
            string query = "SELECT * FROM Medical WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    medical = new Medical()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        ExpirationTime = DateTime.Parse(reader["Expiration_date"].ToString()),
                        Number = int.Parse(reader["Number"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }

            return medical;

        }

        public Salary GetSalary(int id)
        {
            Salary salary = new Salary();
            string query = "SELECT TOP 1 * FROM dbo.Salary  WHERE id = " + id.ToString() + "ORDER BY ID DESC ";
            SqlDataReader reader = Execute(query).ExecuteReader();
          
            try
            {
                while (reader.Read())
                {
                    salary = new Salary()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Base = Decimal.Parse(reader["Base"].ToString()),
                        Bonus = Decimal.Parse(reader["Bonus"].ToString()),
                        Overtime = Decimal.Parse(reader["Overtime"].ToString()),
                        Total = Decimal.Parse(reader["Total"].ToString())
                     };

                }
            }
            finally
            {
                reader.Close();
            }

           
            return salary;

        }

        public Hours GetHours(int id)
        {
            Hours hours = new Hours();
            string query = "SELECT TOP 1 * FROM dbo.Hours  WHERE id = " + id.ToString() + "ORDER BY ID DESC ";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    hours = new Hours()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        HoursWorked = Decimal.Parse(reader["Hours_worked"].ToString()),
                        QuantityOvertime = Decimal.Parse(reader["Quantity_overtime"].ToString())
                    };
                }

            }
            finally
            {
                reader.Close();
            }

         
            return hours;

        }

        public bool IsPasswordCorrect(string username, string password)
        {
            bool isPasswordCorrect = false;
            string query = "SELECT * FROM LOGIN WHERE login = '" + username+ "'";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                     isPasswordCorrect = (reader["Password"].ToString() == password);

                }

            }
            finally
            {
                reader.Close();
            }

            return isPasswordCorrect;

        }

        public bool IsAdmin(string username, string password)
        {
            bool _IsAdmin = false;
            string query = "SELECT admin FROM LOGIN WHERE login = " + username;
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    _IsAdmin = (Boolean.Parse(reader["Admin"].ToString()));

                }

            }
            finally
            {
                reader.Close();
            }

            return _IsAdmin;
        }

        public int GetUserId(int idLogin)
        {
            int result = -1;
            string query = "SELECT ID FROM EMPLOYEER WHERE ID_login  = " + idLogin.ToString() ;
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())

                {
                    result = int.Parse(reader["id"].ToString());
                   
                }

            }
            finally
            {
                reader.Close();
            }

            return result;
        }

        public int GetLoginId(string username)
        {
            int result = -1;
            string query = "SELECT ID FROM Login WHERE  Login= '" + username +"'";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())

                {
                    result = int.Parse(reader["id"].ToString());
                    

                }

            }
            finally
            {
                reader.Close();
            }

            return result;
        
    }


    }


}
