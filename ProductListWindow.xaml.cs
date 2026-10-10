using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp;

public partial class ProductListWindow : Window
{
    private readonly Users _currentUser;
    private readonly ObservableCollection<Products> _products;
    private CollectionViewSource _source;

    public ICollectionView Products { get; set; }

    public ProductListWindow(Users user)
    {
        InitializeComponent();
        DataContext = this;
        _currentUser = user;

        FullNameText.Text = user.FullName;
        RoleText.Text = user.RoleNavigation?.Role ?? "";

        _products = [];
        _source = new() { Source = _products };
        Products = _source.View;
        Products.Filter = ProductFilter;
        ReloadProducts();
    }

    private void ReloadProducts()
    {
        _products.Clear();
        using var db = new ShopDbContext();
        var items = db.Products
            .Include(p => p.Category)
            .Include(p => p.Name)
            .Include(p => p.Manufacturer)
            .Include(p => p.Supplier)
            .ToList();
        foreach (var p in items)
            _products.Add(p);
    }

    // Поиск по всем текстовым полям сразу; несколько слов = совпадение по нескольким атрибутам
    private bool ProductFilter(object obj)
    {
        if (obj is not Products p) return false;
        var query = SearchBox?.Text?.Trim();
        if (string.IsNullOrEmpty(query)) return true;

        string?[] fields =
        [
            p.Article, p.Name?.Name, p.Description,
            p.Category?.Name, p.Manufacturer?.Name,
            p.Supplier?.Name, p.UnitName
        ];

        return query
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .All(word => fields.Any(f =>
                f?.Contains(word, StringComparison.OrdinalIgnoreCase) == true));
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        => Products.Refresh();

    private void Card_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is not FrameworkElement { DataContext: Products p }) return;
        var w = new ProductEditWindow(p.Id) { Owner = this };
        w.ShowDialog();
        if (w.Changed) ReloadProducts();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        var w = new ProductEditWindow(null) { Owner = this };
        w.ShowDialog();
        if (w.Changed) ReloadProducts();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        Close();
    }
}
