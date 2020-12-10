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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FulltimeEmployee FtEmployee1 = new FulltimeEmployee("Ethan", "De Guzman", 500);
            FulltimeEmployee FtEmployee2 = new FulltimeEmployee("Ethan", "De Guzman", 300);
            ParttimeEmployee PtEmployee1 = new ParttimeEmployee("Ethan", "De Guzman", 5, 2);
            ParttimeEmployee PtEmployee2 = new ParttimeEmployee("Ethan", "De Guzman", 4, 3);

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
            if (chkPartTime.IsChecked == true)
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


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstname = tbxFirstName.Text;
            string surname = tbxSurname.Text;
            
            if (btnFT.IsChecked == true)
            {
                decimal salary = decimal.Parse(tbxSalary.Text);
                FulltimeEmployee FtEmployee = new FulltimeEmployee(firstname, surname, salary);
                tblkMonthlyPay.Text = FtEmployee.CalculateMonthlyPay().ToString("€0.##");
                Employees.Add(FtEmployee);
            }
            else
            {
                double hoursworked = double.Parse(tbxHoursWorked.Text);
                decimal hourlyrate = decimal.Parse(tbxHourlyRate.Text);
                ParttimeEmployee PtEmployee = new ParttimeEmployee(firstname, surname, hourlyrate, hoursworked);
                tblkMonthlyPay.Text = PtEmployee.CalculateMonthlyPay().ToString("€0.##");
                Employees.Add(PtEmployee);
            }

        }

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
        }
    }
}
