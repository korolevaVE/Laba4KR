using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShopApp;

// Скидка > 20 -> фон #C8A2C8, иначе белый
public class DiscountToBackgroundConverter : IValueConverter
{
    private static readonly Brush HighlightBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C8A2C8"));
    private static readonly Brush NormalBrush = Brushes.White;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is double d && d > 20 ? HighlightBrush : NormalBrush;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

// Скидка > 0 -> Visible, иначе Collapsed (parameter="Invert" переворачивает)
public class DiscountToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var visible = value is double d && d > 0;
        if (parameter as string == "Invert") visible = !visible;
        return visible ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

// Имя файла картинки -> BitmapImage из папки Images рядом с exe, null если файла нет
public class ProductImageConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string path || string.IsNullOrWhiteSpace(path))
            return null;

        var full = Path.Combine(AppContext.BaseDirectory, "Images", path);
        return File.Exists(full) ? new BitmapImage(new Uri(full)) : null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
