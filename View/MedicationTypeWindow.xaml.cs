using PharmacyApp.Models;
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
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.ViewModel;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для MedicationTypeWindow.xaml
    /// </summary>
    public partial class MedicationTypeWindow : Window
    {
        public MedicationTypeWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IMedicationTypesRepository typeRepository = new MedicationTypesRepository(context);
            DataContext = new MedicationTypeViewModel(typeRepository);
        }
    }
}
