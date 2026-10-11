namespace SrpLab;

public sealed class PickerInstructionFormatter
{
    public string Format(
        IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IReadOnlyList<(string Sku, int Allocated)> allocations,
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        var steps = walkingOrder.Select((item, index) =>
            $"{index + 1}. Go aisle {item.Aisle} bin {item.Bin}: pick {item.Qty} × {item.Sku}");

        var shortfalls = allocations.Where(allocation =>
        {
            var need = lines.First(line => line.Sku == allocation.Sku).QtyNeeded;
            return allocation.Allocated < need;
        });

        var warning = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(item => item.Sku))
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warning;
    }
}