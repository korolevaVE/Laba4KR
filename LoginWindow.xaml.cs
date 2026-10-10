using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;

namespace ShopApp;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
        => LoginBox.Focus();

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        using var db = new ShopDbContext();
        var user = db.Users
            .Include(u => u.RoleNavigation)
            .FirstOrDefault(u => u.Login == LoginBox.Text.Trim()
                              && u.Password == PasswordBox.Password);

        if (user is null)
        {
            MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        new ProductListWindow(user).Show();
        Close();
    }
}
