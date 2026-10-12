using System;

namespace LibrarySystem;

public class Loan
{
    public int LoanId { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public string Status { get; private set; }

    public Loan(int loanId, Member member, LibraryItem item, DateTime borrowDate)
    {
        if (loanId <= 0)
            throw new LibraryDomainException("Loan ID must be greater than zero.");

        if (member == null)
            throw new LibraryDomainException("Member cannot be null.");

        if (item == null)
            throw new LibraryDomainException("Item cannot be null.");

        if (item.IsWithdrawn)
            throw new LibraryDomainException("Cannot loan an item that has been withdrawn from circulation.");

        if (item.IsOnLoan)
            throw new LibraryDomainException("Item is already currently on loan.");

        if (member.Loans.Count >= member.MaxAllowedLoans)
            throw new LibraryDomainException($"Member has reached the maximum allowed limit of {member.MaxAllowedLoans} loans.");

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate;

        DueDate = borrowDate.AddDays(item.GetLoanPeriodDays());
        Status = "Borrowed";

        Item.IsOnLoan = true;
        Member.AddLoan(this);
    }

    public void ReturnItem(DateTime returnDate)
    {
        if (Status != "Borrowed")
            throw new LibraryDomainException("Cannot return a loan that is not currently active.");

        if (returnDate < BorrowDate)
            throw new LibraryDomainException("Return date cannot be earlier than the borrow date.");

        Status = "Returned";
        ReturnDate = returnDate;

        Item.IsOnLoan = false;

        if (Member is PremiumMember premiumMember)
        {
            premiumMember.AddPoints(5);
        }
    }

    public void MarkAsLost()
    {
        if (Status != "Borrowed")
            throw new LibraryDomainException("Cannot mark a loan as lost if it is not currently active.");

        Status = "Lost";

        Item.IsOnLoan = false;
    }

    public decimal CalculateLateFee(DateTime currentDate)
    {
        DateTime effectiveDate = ReturnDate ?? currentDate;

        if (effectiveDate <= DueDate)
            return 0m;

        int overdueDays = (effectiveDate - DueDate).Days;
        decimal dailyFee = Item.GetDailyLateFee();
        decimal totalFee = overdueDays * dailyFee;

        if (Member is PremiumMember premiumMember)
        {
            decimal discountAmount = totalFee * (premiumMember.DiscountPercentage / 100m);
            totalFee -= discountAmount;
        }

        return totalFee;
    }
}