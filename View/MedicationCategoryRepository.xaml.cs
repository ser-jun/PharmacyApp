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
    /// Логика взаимодействия для MedicationCategoryRepository.xaml
    /// </summary>
    public partial class MedicationCategoryRepository : Window
    {
        public MedicationCategoryRepository()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IMedicationCategoryRepository categoryRepository = new MedicationCategoriesRepository(context);
            DataContext = new MedicationCategoryViewModel(categoryRepository);
        }
    }
}
