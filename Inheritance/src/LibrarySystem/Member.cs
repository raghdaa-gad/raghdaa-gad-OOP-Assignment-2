using System.Collections.Generic;

namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new List<Loan>();

    public int MaxAllowedLoans { get; }
    public IReadOnlyList<Loan> Loans => _loans;

    protected Member(int personId, string fullName, string phoneNumber, int maxAllowedLoans)
        : base(personId, fullName, phoneNumber)
    {
        if (maxAllowedLoans <= 0)
            throw new LibraryDomainException("Max allowed loans must be greater than zero.");

        MaxAllowedLoans = maxAllowedLoans;
    }

    public void AddLoan(Loan loan)
    {
        if (loan == null)
            throw new LibraryDomainException("Cannot add a null loan.");

        _loans.Add(loan);
    }
}