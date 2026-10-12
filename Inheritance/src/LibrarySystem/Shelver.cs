using System;

namespace LibrarySystem;

public class Shelver : StaffMember
{
    public string Section { get; private set; }

    public Shelver(int personId, string fullName, string phoneNumber, DateTime hireDate, decimal salary, string section)
        : base(personId, fullName, phoneNumber, hireDate, salary)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new LibraryDomainException("Section cannot be empty.");

        Section = section;
    }

    public void Reassign(string newSection)
    {
        if (string.IsNullOrWhiteSpace(newSection))
            throw new LibraryDomainException("New section cannot be empty.");

        Section = newSection;
    }
}