using System;

namespace LibrarySystem;

public class Librarian : StaffMember
{
    public Librarian(int personId, string fullName, string phoneNumber, DateTime hireDate, decimal salary)
        : base(personId, fullName, phoneNumber, hireDate, salary)
    {
    }
}