using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();
        ObservableCollection<Employee> filteredEmployees = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FulltimeEmployee FtEmployee1 = new FulltimeEmployee("Ethan", "De Guzman", 5000);
            FulltimeEmployee FtEmployee2 = new FulltimeEmployee("Linda", "Martin", 3000);
            ParttimeEmployee PtEmployee1 = new ParttimeEmployee("Barbara", "Wilson", 15, 10);
            ParttimeEmployee PtEmployee2 = new ParttimeEmployee("Mark", "Jones", 13, 15);

            Employees.Add(FtEmployee1);
            Employees.Add(FtEmployee2);

            Employees.Add(PtEmployee1);
            Employees.Add(PtEmployee2);

            lstEmployee.ItemsSource = Employees;

            
        }

        private void lstEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkFullTime.IsChecked == true)
            {
                FulltimeEmployee selectedEmployee = lstEmployee.SelectedItem as FulltimeEmployee;
                if (selectedEmployee != null)
                {
                    tbxFirstName.Text = selectedEmployee.firstname;
                    tbxSurname.Text = selectedEmployee.surname;
                    tbxSalary.Text = Convert.ToString(selectedEmployee.salary);
                    btnFT.IsChecked = true;
                    tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                    tbxHoursWorked.Clear();
                    tbxHourlyRate.Clear();
                }
            }
            else if (chkPartTime.IsChecked == true)
            {
                ParttimeEmployee selectedEmployee = lstEmployee.SelectedItem as ParttimeEmployee;
                if (selectedEmployee != null)
                {
                    tbxFirstName.Text = selectedEmployee.firstname;
                    tbxSurname.Text = selectedEmployee.surname;
                    tbxHoursWorked.Text = Convert.ToString(selectedEmployee.hoursworked);
                    tbxHourlyRate.Text = Convert.ToString(selectedEmployee.hourlyrate);
                    btnPT.IsChecked = false;
                    tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                    tbxSalary.Clear();
                }
            }
        } // Semi Done

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstname = tbxFirstName.Text;
            string surname = tbxSurname.Text;

            if (btnFT.IsChecked == true && tbxSalary.Text != "")
            {
                decimal salary = decimal.Parse(tbxSalary.Text);
                FulltimeEmployee FtEmployee = new FulltimeEmployee(firstname, surname, salary);
                tblkMonthlyPay.Text = FtEmployee.CalculateMonthlyPay().ToString("€0.##");
                Employees.Add(FtEmployee);
            }
            else if (btnPT.IsChecked == true && tbxHourlyRate.Text != "" && tbxHoursWorked.Text != "")
            {
                double hoursworked = double.Parse(tbxHoursWorked.Text);
                decimal hourlyrate = decimal.Parse(tbxHourlyRate.Text);
                ParttimeEmployee PtEmployee = new ParttimeEmployee(firstname, surname, hourlyrate, hoursworked);
                tblkMonthlyPay.Text = PtEmployee.CalculateMonthlyPay().ToString("€0.##");
                Employees.Add(PtEmployee);
            }
            else
            {
                MessageBox.Show("Error Must Fill in all necessary values");
            }
        } //Done

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxFirstName.Clear();
            tbxSurname.Clear();
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            btnFT.IsChecked = false;
            btnPT.IsChecked = false;
            tblkMonthlyPay.Text = "";
        } //Done

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = lstEmployee.SelectedItem as Employee;
            Employees.Remove(selectedEmployee);
        } //Done

        private void btnUpdate_Click(object sender, RoutedEventArgs e) // Needs to be finished once sort is finished
        {
            if (btnFT.IsChecked == true && tbxSalary.Text != "")
            {
                FulltimeEmployee selectedEmployee = lstEmployee.SelectedItem as FulltimeEmployee;
                if (selectedEmployee != null)
                {
                    selectedEmployee.firstname = tbxFirstName.Text;
                    selectedEmployee.surname = tbxSurname.Text;

                    selectedEmployee.salary = decimal.Parse(tbxSalary.Text);
                    tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                    tbxHoursWorked.Clear();
                    tbxHourlyRate.Clear();
                }
            }
            else if (btnPT.IsChecked == true && tbxHourlyRate.Text != "" && tbxHoursWorked.Text != "")
            {
                ParttimeEmployee selectedEmployee = lstEmployee.SelectedItem as ParttimeEmployee;
                if (selectedEmployee != null)
                {
                    selectedEmployee.firstname = tbxFirstName.Text;
                    selectedEmployee.surname = tbxSurname.Text;

                    selectedEmployee.hoursworked = double.Parse(tbxHoursWorked.Text);
                    selectedEmployee.hourlyrate = decimal.Parse(tbxHourlyRate.Text);
                    tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                    tbxSalary.Clear();
                }
            }
            else
            {
                MessageBox.Show("Error Must Select either Full time Employee or Part time Employee");
            }
        }

        private void chkFullTime_Checked(object sender, RoutedEventArgs e) // Done
        {
            filteredEmployees.Clear();
            lstEmployee.ItemsSource = null;

            if (chkFullTime.IsChecked == true)
            {
                foreach (Employee employee in Employees)
                {
                    if (employee is FulltimeEmployee)
                    {
                        filteredEmployees.Add(employee);
                    }
                }
                lstEmployee.ItemsSource = filteredEmployees;
            }
            else
            {
                foreach (Employee employee in Employees)
                {
                    if (employee is ParttimeEmployee)
                    {
                        filteredEmployees.Add(employee);
                    }
                }
                lstEmployee.ItemsSource = filteredEmployees;
            }

        }
    }
}