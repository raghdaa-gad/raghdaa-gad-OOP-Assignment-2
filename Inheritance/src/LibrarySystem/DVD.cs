namespace LibrarySystem;

public class DVD : LibraryItem
{
    public DVD(int catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, baseLateFee)
    {
    }

    public override int GetLoanPeriodDays() => 7;
    public override decimal GetDailyLateFee() => BaseLateFee * 2m;
}