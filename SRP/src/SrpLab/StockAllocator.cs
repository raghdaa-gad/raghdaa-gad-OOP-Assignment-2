namespace SrpLab;

public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .Select(line => (line.Sku, Math.Min(line.QtyNeeded, line.QtyOnHand)))
            .ToList();
    }
}