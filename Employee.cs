using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    public abstract class Employee
    {
        protected string FirstName;
        protected string Surname;

        public string firstname { get { return FirstName; } set { FirstName = value;} }
        public string surname { get { return Surname; } set { Surname = value; } }
        public Employee(string FirstName, string Surname)
        {
            this.FirstName = FirstName;
            this.Surname = Surname;
        }

        public abstract decimal CalculateMonthlyPay();

        public abstract override string ToString();

    }

    public class FulltimeEmployee : Employee
    {
        private decimal Salary;
        public decimal salary 
        {
            get { return Salary; } 
            set { Salary = value; }
        }

        public FulltimeEmployee(string FirstName, string Surname, decimal salary) : base(FirstName, Surname)
        {
            this.salary = salary;
        }

        public override decimal CalculateMonthlyPay()
        {
            return salary / 12;
        }

        public override string ToString()
        {
            return string.Format($"{Surname.ToUpper()}, {FirstName} - Full Time");
        }
    }

    public class ParttimeEmployee : Employee
    {
        private decimal HourlyRate;
        private double HoursWorked;
        public decimal hourlyrate
        {
            get { return HourlyRate; }
            set { HourlyRate = value; }
        }

        public double hoursworked
        {
            get { return HoursWorked; }
            set { HoursWorked = value; }
        }

        public ParttimeEmployee(string FirstName, string Surname, decimal HourlyRate, double HoursWorked) : base(FirstName, Surname)
        {
            this.HourlyRate = HourlyRate;
            this.HoursWorked = HoursWorked;
        }

        public override decimal CalculateMonthlyPay()
        {
            return HourlyRate * (decimal) HoursWorked;
        }

        public override string ToString()
        {
            return string.Format($"{Surname.ToUpper()}, {FirstName} - Part Time");
        }
    }
}
