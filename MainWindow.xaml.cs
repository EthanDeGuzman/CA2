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

    /*==============================================================                                              
        GitHub Link = https://github.com/EthanDeguzman/CA2 
    ==============================================================*/

    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>(); // Observable Collection that contains all Employees
        ObservableCollection<Employee> filteredEmployees = new ObservableCollection<Employee>(); // Observable Collection that contains the filtered employees, This is sorted alphabetically through surname

        public MainWindow()
        {
            InitializeComponent(); // Starts the window
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // When the Window is Loaded execute this code
        {
            // Adding Two Full Time Employees
            FulltimeEmployee FtEmployee1 = new FulltimeEmployee("Ethan", "De Guzman", 5000);
            FulltimeEmployee FtEmployee2 = new FulltimeEmployee("Linda", "Martin", 3000);

            // Adding Two Part Time Employees
            ParttimeEmployee PtEmployee1 = new ParttimeEmployee("Barbara", "Wilson", 15, 10);
            ParttimeEmployee PtEmployee2 = new ParttimeEmployee("Mark", "Jones", 13, 15);

            Employees.Add(FtEmployee1);
            Employees.Add(FtEmployee2);

            Employees.Add(PtEmployee1);
            Employees.Add(PtEmployee2);

            Sort(); // Calls The Sort Method which will sort the Employees surname alphabetically
        }

        private void lstEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e) // Checks when an object is selected in the list box
        {
            Employee employee = lstEmployee.SelectedItem as Employee;
            if (employee != null) // Make sure selectedEmployee is not null to avoid errors
            {
                if (employee is FulltimeEmployee) // If the employee selected is a Full Time Employee execute this
                {
                    FulltimeEmployee selectedEmployee = lstEmployee.SelectedItem as FulltimeEmployee;
                        // Displays the Details on the respective text box and buttons
                        tbxFirstName.Text = selectedEmployee.firstname;
                        tbxSurname.Text = selectedEmployee.surname;
                        tbxSalary.Text = Convert.ToString(selectedEmployee.salary);
                        btnFT.IsChecked = true;
                        tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                        //Clears Unneccessary Details
                        tbxHoursWorked.Clear();
                        tbxHourlyRate.Clear();
                }
                else if (employee is ParttimeEmployee)
                {
                    ParttimeEmployee selectedEmployee = lstEmployee.SelectedItem as ParttimeEmployee;
                    tbxFirstName.Text = selectedEmployee.firstname;
                    tbxSurname.Text = selectedEmployee.surname;
                    tbxHoursWorked.Text = Convert.ToString(selectedEmployee.hoursworked);
                    tbxHourlyRate.Text = Convert.ToString(selectedEmployee.hourlyrate);
                    btnPT.IsChecked = true;
                    tblkMonthlyPay.Text = selectedEmployee.CalculateMonthlyPay().ToString("€0.##");

                    tbxSalary.Clear();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) // When Button is click Add a new employee
        {
            // Variables that holds the name of the employee
            string firstname = tbxFirstName.Text;
            string surname = tbxSurname.Text;

            if (btnFT.IsChecked == true && tbxSalary.Text != "") // Makes sure Salary is not null
            {
                decimal salary = decimal.Parse(tbxSalary.Text); // Parses the Text value of the Salary Text box to a decimal
                FulltimeEmployee FtEmployee = new FulltimeEmployee(firstname, surname, salary); // Creates a new FullTime Employee
                tblkMonthlyPay.Text = FtEmployee.CalculateMonthlyPay().ToString("€0.##"); // Calculates the MonthlyPay and Formats it to two decimal places
                Employees.Add(FtEmployee); // Adds the new Employee to the Employee Collection
                Filter(); // Calls Filter method to check if filtering is needed (Sort Method is inside Filter method to automatically Sort)
            }
            else if (btnPT.IsChecked == true && tbxHourlyRate.Text != "" && tbxHoursWorked.Text != "") // Makes sure Hourly Rate and Hours Worked is not null
            {
                // Similar to last if statement
                double hoursworked = double.Parse(tbxHoursWorked.Text);
                decimal hourlyrate = decimal.Parse(tbxHourlyRate.Text);
                ParttimeEmployee PtEmployee = new ParttimeEmployee(firstname, surname, hourlyrate, hoursworked);
                tblkMonthlyPay.Text = PtEmployee.CalculateMonthlyPay().ToString("€0.##");
                Employees.Add(PtEmployee);
                Filter();
            }
            else
            {
                MessageBox.Show("Error Must Fill in all necessary values"); // Gives out an Error if required values aren't entered
            }

        } 

        private void btnClear_Click(object sender, RoutedEventArgs e) // When Button is click clear all fields so user can enter new details
        {
            // Clears all the text boxes and buttons
            tbxFirstName.Clear();
            tbxSurname.Clear();
            tbxSalary.Clear();
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            btnFT.IsChecked = false;
            btnPT.IsChecked = false;
            tblkMonthlyPay.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // When Button is click delete the selected employee
        {
            Employee selectedEmployee = lstEmployee.SelectedItem as Employee;
            Employees.Remove(selectedEmployee); // Removes the selected employee from the list
            Filter(); // Calls filter method
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) // When Button is click update the details of the selected employee
        {
            // Similar code to the Add_Click Event but this time we set the values of the text box to the variables to update them
            Employee employee = lstEmployee.SelectedItem as Employee;
            if(employee != null)
            {
                if (employee is FulltimeEmployee && tbxSalary.Text != "")
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
                        Filter();
                    }
                }
                else if (employee is ParttimeEmployee && tbxHourlyRate.Text != "" && tbxHoursWorked.Text != "")
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
                        Filter();
                    }
                }
                else
                {
                    MessageBox.Show("Error Must Select either Full time Employee or Part time Employee");
                }
            }
        }

        private void chkFullTime_Click(object sender, RoutedEventArgs e) // Starts Filtering when clicked
        {
            Filter();
        }
        private void Filter() // Filter Method that checks if the list needs filtering and filters it if needed
        {
            filteredEmployees.Clear(); // Clears the collection filtered employees to make sure nothing is in it
            lstEmployee.ItemsSource = null; // Clears the List box so we can update it

            if (chkFullTime.IsChecked == true && chkPartTime.IsChecked == true) // If both checkbox are ticked then display all employees
            {
                foreach (Employee employee in Employees)
                {
                    filteredEmployees.Add(employee);
                }
                lstEmployee.ItemsSource = filteredEmployees; // Adds employees back into the list
            }
            else if (chkFullTime.IsChecked == true) // if FT check box is clicked execute
            {
                foreach (Employee employee in Employees)
                {
                    if (employee is FulltimeEmployee)
                    {
                        filteredEmployees.Add(employee);
                    }
                }
                lstEmployee.ItemsSource = filteredEmployees; // Adds filtered employees back to the list
            }
            else // Similar to if statement
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

            Sort(); // Calls the sort method to make sure the list is always sorted alphabetically after filtering
        }

        private void Sort() // Sort method that sorts the list alphabetically through surname. Checks if we should print out filtered list or All employee list
        {
            if (chkFullTime.IsChecked == true || chkPartTime.IsChecked == true) // If any filtering is active make sure to sort the filtering list and print that out
            {
                lstEmployee.ItemsSource = null;
                filteredEmployees = new ObservableCollection<Employee>(filteredEmployees.OrderBy(x => x.surname)); // Sorts new collection alphabetically by surname
                lstEmployee.ItemsSource = filteredEmployees;
            }
            else // Similar to if statement but we sort through employees not filtered
            {
                lstEmployee.ItemsSource = null;
                Employees = new ObservableCollection<Employee>(Employees.OrderBy(x => x.surname));
                lstEmployee.ItemsSource = Employees;
            }
        }
    }
}