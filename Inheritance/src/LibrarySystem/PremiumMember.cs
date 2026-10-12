namespace LibrarySystem;

public class PremiumMember : Member
{
    public decimal DiscountPercentage { get; }
    public int ReadingPoints { get; private set; }

    public PremiumMember(int personId, string fullName, string phoneNumber, decimal discountPercentage)
        : base(personId, fullName, phoneNumber, 10)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new LibraryDomainException("Discount percentage must be between 0 and 100.");

        DiscountPercentage = discountPercentage;
        ReadingPoints = 0;
    }

    public void AddPoints(int points)
    {
        if (points <= 0)
            throw new LibraryDomainException("Points to add must be greater than zero.");

        ReadingPoints += points;
    }
}