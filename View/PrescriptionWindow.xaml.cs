using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.Repositories;
using PharmacyApp.ViewModel;


namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для PrescriptionWindow.xaml
    /// </summary>
    public partial class PrescriptionWindow : Window
    {
        public PrescriptionWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IPrescriptionrepository prescriptionRepositoyr = new PrescriptionRepository(context);
            ICrudRepository<Doctor> doctorcrudRepository = new CrudRepository<Doctor>(context);
            ICrudRepository<Customer> customerCrudRepository = new CrudRepository<Customer>(context);
            DataContext = new PrescriptionViewModel(prescriptionRepositoyr, customerCrudRepository, doctorcrudRepository);
        }
    }
}
