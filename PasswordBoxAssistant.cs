using System.Windows;
using System.Windows.Controls;

namespace PharmacyApp
{
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        public static readonly DependencyProperty BoundPassword = DependencyProperty.RegisterAttached(
            "BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword = DependencyProperty.RegisterAttached(
            "UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        public static void SetBindPassword(DependencyObject dp, bool value) => dp.SetValue(BindPassword, value);
        public static bool GetBindPassword(DependencyObject dp) => (bool)dp.GetValue(BindPassword);

        public static void SetBoundPassword(DependencyObject dp, string value) => dp.SetValue(BoundPassword, value);
        public static string GetBoundPassword(DependencyObject dp) => (string)dp.GetValue(BoundPassword);

        private static bool GetUpdatingPassword(DependencyObject dp) => (bool)dp.GetValue(UpdatingPassword);
        private static void SetUpdatingPassword(DependencyObject dp, bool value) => dp.SetValue(UpdatingPassword, value);

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            if (!(dp is PasswordBox passwordBox))
                return;

            passwordBox.PasswordChanged -= HandlePasswordChanged;

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            if (!(dp is PasswordBox passwordBox))
                return;

            if (GetUpdatingPassword(passwordBox))
                return; 

            passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;

            SetUpdatingPassword(passwordBox, true);
            SetBoundPassword(passwordBox, passwordBox.Password);
            SetUpdatingPassword(passwordBox, false);
        }
    }
}