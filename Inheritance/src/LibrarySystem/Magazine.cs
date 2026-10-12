namespace LibrarySystem;

public class Magazine : LibraryItem
{
    public Magazine(int catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, baseLateFee)
    {
    }

    public override int GetLoanPeriodDays() => 3;
    public override decimal GetDailyLateFee() => BaseLateFee / 2m;
}