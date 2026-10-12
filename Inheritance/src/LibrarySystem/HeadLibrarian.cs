using System;

namespace LibrarySystem;

public class HeadLibrarian : StaffMember
{
    public HeadLibrarian(int personId, string fullName, string phoneNumber, DateTime hireDate, decimal salary)
        : base(personId, fullName, phoneNumber, hireDate, salary)
    {
    }

    public override decimal GetMonthlyPay()
    {
        return MonthlySalary + 400m;
    }
}