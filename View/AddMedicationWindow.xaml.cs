using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
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
using PharmacyApp.Repositories;
using PharmacyApp.ViewModel;
using PharmacyApp.DTO;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddMedicationWindow.xaml
    /// </summary>
    public partial class AddMedicationWindow : Window
    {
        public AddMedicationWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IMedicationFormAddEdit medicationForm = new MedicationsRepository(context);
            DataContext = new AddEditMedicationWindowViewModel(medicationForm);
        }
        public AddMedicationWindow(MedicationsItems medicationToEdit)
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IMedicationFormAddEdit medicationForm = new MedicationsRepository(context);
            DataContext = new AddEditMedicationWindowViewModel(medicationForm,  medicationToEdit);
        }
    }
}
