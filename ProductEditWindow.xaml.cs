using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp;

public partial class ProductEditWindow : Window
{
    private readonly int? _productId;

    // true, если список товаров нужно перезагрузить
    public bool Changed { get; private set; }

    // null -> добавление, иначе редактирование товара с этим Id
    public ProductEditWindow(int? productId)
    {
        InitializeComponent();
        _productId = productId;
        Loaded += Window_Loaded;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        using var db = new ShopDbContext();

        NameBox.ItemsSource = db.ProductNames.OrderBy(n => n.Name).ToList();
        CategoryBox.ItemsSource = db.Categories.OrderBy(c => c.Name).ToList();
        ManufacturerBox.ItemsSource = db.Manufacturers.OrderBy(m => m.Name).ToList();
        SupplierBox.ItemsSource = db.Suppliers.OrderBy(s => s.Name).ToList();

        if (_productId.HasValue)
        {
            Title = "Редактирование товара";
            var p = db.Products.Include(x => x.Name).First(x => x.Id == _productId.Value);

            IdBox.Text = p.Id.ToString();
            NameBox.Text = p.Name?.Name ?? "";
            CategoryBox.SelectedValue = p.CategoryId;
            DescriptionBox.Text = p.Description ?? "";
            ManufacturerBox.SelectedValue = p.ManufacturerId;
            SupplierBox.SelectedValue = p.SupplierId;
            PriceBox.Text = (p.Price ?? 0).ToString("0.##");
            CountBox.Text = (p.Count ?? 0).ToString("0.##");
            DiscountBox.Text = (p.Discount ?? 0).ToString("0.##");
        }
        else
        {
            Title = "Добавление товара";
            IdLabel.Visibility = Visibility.Collapsed;
            IdBox.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }
    }

    private static bool TryParseNonNegative(string text, out double value)
    {
        var s = text.Trim().Replace(',', '.');
        return double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out value)
               && value >= 0;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var name = NameBox.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Введите наименование товара", "Проверка данных",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (CategoryBox.SelectedValue is null || ManufacturerBox.SelectedValue is null
            || SupplierBox.SelectedValue is null)
        {
            MessageBox.Show("Выберите категорию, производителя и поставщика", "Проверка данных",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!TryParseNonNegative(PriceBox.Text, out var price))
        {
            MessageBox.Show("Цена должна быть неотрицательным числом", "Проверка данных",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!TryParseNonNegative(CountBox.Text, out var count))
        {
            MessageBox.Show("Количество должно быть неотрицательным числом", "Проверка данных",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!TryParseNonNegative(DiscountBox.Text, out var discount) || discount > 100)
        {
            MessageBox.Show("Скидка должна быть числом от 0 до 100", "Проверка данных",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        using var db = new ShopDbContext();

        var productName = db.ProductNames.FirstOrDefault(n => n.Name == name);
        if (productName is null)
        {
            productName = new ProductNames
            {
                Id = (db.ProductNames.Max(n => (int?)n.Id) ?? 0) + 1,
                Name = name
            };
            db.ProductNames.Add(productName);
        }

        Products p;
        if (_productId.HasValue)
        {
            p = db.Products.First(x => x.Id == _productId.Value);
        }
        else
        {
            p = new Products
            {
                Id = (db.Products.Max(x => (int?)x.Id) ?? 0) + 1,
                Unit = "1",
                Article = ""
            };
            db.Products.Add(p);
        }

        p.NameId = productName.Id;
        p.CategoryId = (int)CategoryBox.SelectedValue;
        p.Description = DescriptionBox.Text.Trim();
        p.ManufacturerId = (int)ManufacturerBox.SelectedValue;
        p.SupplierId = (int)SupplierBox.SelectedValue;
        p.Price = price;
        p.Count = count;
        p.Discount = discount;

        db.SaveChanges();
        Changed = true;
        Close();
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Удалить этот товар?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;

        using var db = new ShopDbContext();
        if (db.OrderArticle.Any(o => o.ProductId == _productId!.Value))
        {
            MessageBox.Show("Товар присутствует в заказе, удалить его нельзя", "Удаление",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var p = db.Products.First(x => x.Id == _productId!.Value);
        db.Products.Remove(p);
        db.SaveChanges();
        Changed = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
        => Close();
}
