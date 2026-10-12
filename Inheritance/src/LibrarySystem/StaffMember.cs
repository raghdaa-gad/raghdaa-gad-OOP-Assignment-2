using System;

namespace LibrarySystem;

public class StaffMember : Person
{
    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }

    protected StaffMember(int personId, string fullName, string phoneNumber, DateTime hireDate, decimal salary)
        : base(personId, fullName, phoneNumber)
    {
        if (salary <= 0)
            throw new LibraryDomainException("Salary must be greater than zero.");

        HireDate = hireDate;
        MonthlySalary = salary;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new LibraryDomainException("Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * (percentage / 100m);
    }

    public virtual decimal GetMonthlyPay()
    {
        return MonthlySalary;
    }
}