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
    /// Логика взаимодействия для PendingOrderWindow.xaml
    /// </summary>
    public partial class PendingOrderWindow : Window
    {
        public PendingOrderWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IPendingOrderRepository pendingOrderRepository = new OrderRepository(context);
            ICrudRepository<Models.Component> componentRepository = new CrudRepository<Models.Component>(context);
            DataContext = new OrderViewModel(pendingOrderRepository, componentRepository);
        }
    }
}
