using System;

namespace LibrarySystem;

public class LibraryItem
{
    public int CatalogNumber { get; }
    public string Title { get; }
    public decimal BaseLateFee { get; private set; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; internal set; }

    protected LibraryItem(int catalogNumber, string title, decimal baseLateFee)
    {
        if (catalogNumber <= 0)
            throw new LibraryDomainException("Catalog number must be greater than zero.");

        if (string.IsNullOrWhiteSpace(title))
            throw new LibraryDomainException("Title cannot be empty.");

        if (baseLateFee <= 0)
            throw new LibraryDomainException("Base late fee must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        IsWithdrawn = false;
        IsOnLoan = false;
    }

    public void UpdateBaseFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new LibraryDomainException("New base fee must be greater than zero.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan)
            throw new LibraryDomainException("Cannot withdraw an item that is currently on loan.");

        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    public virtual int GetLoanPeriodDays() => 0;

    public virtual decimal GetDailyLateFee() => BaseLateFee;
}