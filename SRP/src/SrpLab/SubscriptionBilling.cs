namespace SrpLab;

public sealed class SubscriptionBilling
{
    private readonly ProrationCalculator _prorationCalculator = new();
    private readonly InvoiceNumberGenerator _invoiceGenerator = new();
    private readonly DunningEmailFormatter _emailFormatter = new();
    private readonly LedgerJournalFormatter _ledgerFormatter = new();

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(
        string customerId,
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        return _prorationCalculator.Calculate(
            MonthlyPrice, PeriodStart, PeriodEnd, activeFrom);
    }

    public string NextInvoiceNumber()
    {
        return _invoiceGenerator.Next(PeriodStart);
    }

    public void RegisterFailedPayment()
    {
        FailedPayments++;
    }

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();

        return _emailFormatter.Format(
            customerName, amount, invoice, asOf, FailedPayments);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        var invoice = NextInvoiceNumber();
        var amount = Prorate(activeFrom);

        return _ledgerFormatter.Format(CustomerId, invoice, amount);
    }
}