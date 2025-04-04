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
using PharmacyApp.Repositories.Interfaces;
using PharmacyApp.Repositories;
using PharmacyApp.ViewModel;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для SupplierWindow.xaml
    /// </summary>
    public partial class SupplierWindow : Window
    {
        public SupplierWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            ISupplierRepository supplierRepository = new SupplierRepository(context);
            ICrudRepository<Models.Component> crudComponent = new CrudRepository<Models.Component>(context);
            DataContext = new SupplierViewModel(supplierRepository, crudComponent, context);
        }
    }
}
