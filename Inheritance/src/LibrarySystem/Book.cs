namespace LibrarySystem;

public class Book : LibraryItem
{
    public Book(int catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, baseLateFee)
    {
    }

    public override int GetLoanPeriodDays() => 21;
    public override decimal GetDailyLateFee() => BaseLateFee;
}