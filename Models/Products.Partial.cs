namespace ShopApp.Models;

public partial class Products
{
    public double FinalPrice => (Price ?? 0) * (1 - (Discount ?? 0) / 100.0);

    public string UnitName => Unit == "1" ? "шт." : Unit ?? "";
}
