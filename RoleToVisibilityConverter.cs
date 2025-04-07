using PharmacyApp.Services;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PharmacyApp
{
    public class RoleToVisibilityConverter : IValueConverter
    {
        public static RoleToVisibilityConverter Default { get; } = new RoleToVisibilityConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string allowedRoles = parameter?.ToString() ?? "";

            if (string.IsNullOrEmpty(allowedRoles))
                return Visibility.Visible;

            if (!CurrentUser.IsAuthenticated)
                return Visibility.Collapsed;

            string[] roles = allowedRoles.Split(',');
            foreach (var role in roles)
            {
                if (role.Trim() == "admin" && CurrentUser.IsAdmin)
                    return Visibility.Visible;

                if (role.Trim() == "pharmacist" && CurrentUser.IsPharmacist)
                    return Visibility.Visible;

                if (role.Trim() == "customer" && CurrentUser.IsCustomer)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
