using PharmacyApp.Models;
using PharmacyApp.Repositories;
using PharmacyApp.Repositories.Interfaces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PharmacyApp.ViewModel;
using PharmacyApp.Services;

namespace PharmacyApp
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IUserRepository userRepository = new UsersRepository(context);
            DataContext = new UserViewModel(userRepository);
        }
    }
}