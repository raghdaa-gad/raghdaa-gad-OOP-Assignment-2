
namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(
        string fromName,
        IEnumerable<(string Sku, decimal Price, int Qty)> lines,
        decimal grandTotal)
    {
        var items = string.Join(", ", lines.Select(line => line.Sku));

        return $"Dear friend,\n" +
               $"A gift from {fromName} awaits ({items}).\n" +
               $"Total surprise value: {grandTotal:C}\n";
    }
}
