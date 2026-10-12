namespace SrpLab;

public sealed class LedgerJournalFormatter
{
    public string Format(
        string customerId,
        string invoice,
        decimal amount)
    {
        return $"{customerId},{invoice},{amount:0.00},AR-SUB";
    }
}