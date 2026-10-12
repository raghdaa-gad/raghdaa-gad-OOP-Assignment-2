
namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private readonly BasketTotalCalculator _totalCalculator = new();
    private readonly GiftMessageFormatter _giftFormatter = new();
    private readonly PaymentAuthorizationStub _paymentAuthorization = new();

    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _totalCalculator.SubTotal(_lines);
    }

    public decimal DiscountAmount()
    {
        return new CouponDiscountCalculator().Calculate(_couponRaw, SubTotal());
    }

    public decimal GrandTotal()
    {
        return _totalCalculator.GrandTotal(_lines, _couponRaw, _giftWrap);
    }

    public string GiftMessageCard(string fromName)
    {
        return _giftFormatter.Format(fromName, _lines, GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorization.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
