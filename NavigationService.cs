using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PharmacyApp
{
    public static class NavigationService
    {
        public static void OpenWindow<T>() where T : Window, new()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            var newWindow = new T();

            if (currentWindow != null)
            {
                currentWindow.Close();
            }

            newWindow.Show();

        }
        public static T OpenForm<T> () where T : Window, new()
        {
            var newWindow = new T();
            newWindow.Show();
            return newWindow;
        }
    }
}
