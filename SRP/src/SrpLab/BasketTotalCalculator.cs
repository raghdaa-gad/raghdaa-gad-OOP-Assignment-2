
namespace SrpLab;

public sealed class BasketTotalCalculator
{
    private readonly CouponDiscountCalculator _discountCalculator = new();

    public decimal SubTotal(
        IEnumerable<(string Sku, decimal Price, int Qty)> lines)
    {
        return lines.Sum(line => line.Price * line.Qty);
    }

    public decimal GrandTotal(
        IEnumerable<(string Sku, decimal Price, int Qty)> lines,
        string? couponRaw,
        bool giftWrap)
    {
        var subTotal = SubTotal(lines);
        var discount = _discountCalculator.Calculate(couponRaw, subTotal);

        var total = subTotal - discount;

        if (giftWrap)
            total += 4.99m;

        return Math.Max(0m, total);
    }
}
