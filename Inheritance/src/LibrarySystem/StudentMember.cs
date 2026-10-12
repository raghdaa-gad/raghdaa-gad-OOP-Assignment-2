namespace LibrarySystem;

public class StudentMember : Member
{
    public StudentMember(int personId, string fullName, string phoneNumber)
        : base(personId, fullName, phoneNumber, 3)
    {
    }
}