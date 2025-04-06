using PharmacyApp.Models;
using PharmacyApp.Repositories;
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
using PharmacyApp.ViewModel;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для InformationCustomerOrders.xaml
    /// </summary>
    public partial class InformationCustomerOrders : Window
    {
        public InformationCustomerOrders()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            ICustomerOrders customerOrders = new OrderRepository(context);
            DataContext = new CustomerOrdersViewModel(customerOrders);
        }
    }
}
