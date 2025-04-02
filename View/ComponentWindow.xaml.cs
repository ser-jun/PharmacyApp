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
using PharmacyApp.ViewModel;
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.Repositories;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для ComponentWindow.xaml
    /// </summary>
    public partial class ComponentWindow : Window
    {
        public ComponentWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IComponentRepository componentRepository = new ComponentRepository(context);
            DataContext = new ComponentViewModel(componentRepository);
        }
    }
}
