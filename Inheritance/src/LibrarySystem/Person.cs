using System;

namespace LibrarySystem;

public class Person
{
    public int PersonId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    protected Person(int personId, string fullName, string phoneNumber)
    {
        if (personId <= 0)
            throw new LibraryDomainException("Person ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new LibraryDomainException("Full name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new LibraryDomainException("Phone number cannot be empty.");

        PersonId = personId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }
}