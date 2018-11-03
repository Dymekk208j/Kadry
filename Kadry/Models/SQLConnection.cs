using System;
using System.ComponentModel;
using System.Data.SqlClient;


namespace Kadry.Models
{
    public class SQLConnection
    {
        // private string connectionString = "Data Source=DESKTOP-VR8CI70;Initial Catalog=PortfolioWebAppV2;Integrated Security=True;Pooling=False";
        //string connectionString = @"Server=.\DESKTOP-VR8CI70;Database=Kadry;Integrated Security=True;";
        private readonly string connectionString =
            "Data Source=DESKTOP-VR8CI70;Initial Catalog=Kadry;Integrated Security=True;Pooling=False";

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
                        Firstname = reader["First_name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        MiddleName = reader["Middle_name"].ToString(),
                        Pesel = reader["Pesel"].ToString(),
                        Birthday = DateTime.Parse(reader["Birth_date"].ToString()),
                        
                    };
                    int i = Int32.Parse(reader["Sex"].ToString());

                    employer.Sex = i != 0;
                    var idContractType = Int32.Parse(reader["id_Contract_type"].ToString());
                    var idWorkplace = Int32.Parse(reader["id_Workplace"].ToString());
                    var idHoliday = Int32.Parse(reader["id_Holiday"].ToString());
                    var idMedical = Int32.Parse(reader["id_Medical"].ToString());
                    var idSalary = Int32.Parse(reader["id_Salary"].ToString());
                    var idHours = Int32.Parse(reader["id_Hours"].ToString());

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
                        Id = Int32.Parse(reader["id"].ToString()),
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
                        Id = Int32.Parse(reader["id"].ToString()),
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
                        Id = Int32.Parse(reader["id"].ToString()),
                        HolidayAnuses = Int32.Parse(reader["Holiday_anuses"].ToString()),
                        RemainingHoliday = Int32.Parse(reader["Remaining_holiday"].ToString()),
                        Year = Int32.Parse(reader["Year"].ToString())
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
                        Id = Int32.Parse(reader["id"].ToString()),
                        ExpirationTime = DateTime.Parse(reader["Expiration_date"].ToString()),
                        Number = Int32.Parse(reader["Number"].ToString())
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
            string query = "SELECT * FROM Salary WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            var monthId = "";

            try
            {
                while (reader.Read())
                {
                    salary = new Salary()
                    {
                        Id = Int32.Parse(reader["id"].ToString()),
                        Base = Decimal.Parse(reader["Base"].ToString()),
                        Bonus = Decimal.Parse(reader["Bonus"].ToString()),
                        Overtime = Decimal.Parse(reader["Overtime"].ToString()),
                        Total = Decimal.Parse(reader["Total"].ToString())
                     };

                    monthId = reader["id_Month"].ToString();
                }
            }
            finally
            {
                reader.Close();
            }

            salary.Month = GetMonth(Int32.Parse(monthId));

            return salary;

        }

        public Month GetMonth(int id)
        {
            Month month = new Month();
            string query = "SELECT * FROM Month WHERE id = " + id.ToString();
            SqlDataReader executeReader = Execute(query).ExecuteReader(); //executeReader, nowa nazwa, bo inaczej niewiadomo do czego się odwołuje

            try
            {
                while (executeReader.Read())
                {
                    month = new Month()
                    {
                        Id = Int32.Parse(executeReader["id"].ToString()),
                        Name = executeReader["Name"].ToString(),
                        NumberOfMonth = Int32.Parse(executeReader["Number_of_month"].ToString()),
                    };
                }

            }
            finally
            {
                executeReader.Close();
            }

            return month;

        }

        public Hours GetHours(int id)
        {
            Hours hours = new Hours();
            var monthId = "";
            string query = "SELECT * FROM Hours WHERE id = " + id.ToString();
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    hours = new Hours()
                    {
                        Id = Int32.Parse(reader["id"].ToString()),
                        HoursWorked = Decimal.Parse(reader["Hours_worked"].ToString()),
                        QuantityOvertime = Decimal.Parse(reader["Quantity_overtime"].ToString())
                    };
                    monthId = reader["id_Month"].ToString();
                    
                }

            }
            finally
            {
                reader.Close();
            }

            hours.Month = GetMonth(Int32.Parse(monthId));
            return hours;

        }

        public bool IsPasswordCorrect(string username, string password)
        {
            bool _IsPasswordCorrect = false;
            string query = "SELECT * FROM LOGIN WHERE login = '" + username+ "'";
            SqlDataReader reader = Execute(query).ExecuteReader();
            try
            {
                while (reader.Read())
                {
                     _IsPasswordCorrect = (reader["Password"].ToString() == password);

                }

            }
            finally
            {
                reader.Close();
            }

            return _IsPasswordCorrect;

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
                    result = Int32.Parse(reader["id"].ToString());
                   
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
                    result = Int32.Parse(reader["id"].ToString());
                    

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
