using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("   LIBRARY MANAGEMENT SYSTEM - RULE PROOF SIMULATION");
        Console.WriteLine("=================================================\n");

        // -----------------------------------------------------------------
        // 1. MUST NOT COMPILE (Commented-out lines to demonstrate encapsulation)
        // -----------------------------------------------------------------
        // Person p = new Person(1, "Test", "0123");                       // Error: Constructor is protected
        // Member m = new Member(2, "Test", "0123", 5);                    // Error: Constructor is protected
        // StaffMember s = new StaffMember(3, "Test", "0123", DateTime.Now, 1000); // Error: Constructor is protected
        // LibraryItem item = new LibraryItem(4, "Test", 2.0m);           // Error: Constructor is protected
        // student.FullName = "New Name";                                  // Error: FullName is read-only (get only)
        // student.Loans.Add(loan);                                       // Error: Loans is IReadOnlyList
        // book1.IsOnLoan = true;                                          // Error: IsOnLoan setter is internal/private

        // -----------------------------------------------------------------
        // 2. POLYMORPHISM TEST 1: Staff Pay Check
        // -----------------------------------------------------------------
        Console.WriteLine("--- 1. Polymorphism: Staff Monthly Pay ---");
        List<StaffMember> staffList = new List<StaffMember>
        {
            new Librarian(501, "Ayman Ali", "01011112222", DateTime.Now.AddYears(-3), 4000m),
            new HeadLibrarian(502, "Dr. Sarah", "01122223333", DateTime.Now.AddYears(-5), 6000m),
            new Shelver(503, "Mahmoud Hassan", "01233334444", DateTime.Now.AddYears(-1), 3000m, "Fiction")
        };

        foreach (StaffMember staff in staffList)
        {
            Console.WriteLine($"{staff.FullName} ({staff.GetType().Name}) - Monthly Pay: {staff.GetMonthlyPay()} EGP");
        }
        Console.WriteLine();

        // -----------------------------------------------------------------
        // 3. POLYMORPHISM TEST 2: Items Loan Period & Daily Late Fees
        // -----------------------------------------------------------------
        Console.WriteLine("--- 2. Polymorphism: Item Loan Period & Daily Fee ---");
        List<LibraryItem> itemList = new List<LibraryItem>
        {
            new Book(101, "Clean Code", 2.0m),
            new DVD(102, "Inception DVD", 5.0m),
            new Magazine(103, "Tech Monthly", 4.0m)
        };

        foreach (LibraryItem item in itemList)
        {
            Console.WriteLine($"{item.Title} ({item.GetType().Name}): Loan Period = {item.GetLoanPeriodDays()} Days | Daily Fee = {item.GetDailyLateFee()} EGP");
        }
        Console.WriteLine();

        // -----------------------------------------------------------------
        // 4. BUSINESS RULES: Premium Member Late Return & Points Calculation
        // -----------------------------------------------------------------
        Console.WriteLine("--- 3. Business Rules: Premium Member Returning DVD 5 Days Late ---");
        PremiumMember premiumMember = new PremiumMember(1, "Omar Farouk", "01099998888", 10m); // 10% Discount
        DVD testDvd = new DVD(201, "Interstellar DVD", 10.0m); // Base Fee = 10 -> Daily Fee = 20

        DateTime borrowDate = DateTime.Now.AddDays(-12); // DVD loan period is 7 days, so 12 - 7 = 5 days late
        Loan premiumLoan = new Loan(7001, premiumMember, testDvd, borrowDate);

        DateTime returnDate = DateTime.Now;
        premiumLoan.ReturnItem(returnDate);

        decimal lateFee = premiumLoan.CalculateLateFee(returnDate);

        Console.WriteLine($"Member: {premiumMember.FullName}");
        Console.WriteLine($"Item: {testDvd.Title}");
        Console.WriteLine($"Borrow Date: {premiumLoan.BorrowDate:yyyy-MM-dd}");
        Console.WriteLine($"Due Date: {premiumLoan.DueDate:yyyy-MM-dd}");
        Console.WriteLine($"Return Date: {premiumLoan.ReturnDate:yyyy-MM-dd}");
        Console.WriteLine($"Calculated Late Fee (with 10% discount): {lateFee} EGP");
        Console.WriteLine($"Accumulated Reading Points: {premiumMember.ReadingPoints}");
        Console.WriteLine();

        // -----------------------------------------------------------------
        // 5. REJECTION TESTS: Exception Handling Demonstration
        // -----------------------------------------------------------------
        Console.WriteLine("--- 4. Rejection Tests (Catching Failures) ---");

        // Test A: Borrow a Withdrawn Item
        try
        {
            Console.Write("Attempting to borrow a withdrawn item: ");
            Book withdrawnBook = new Book(104, "Outdated Manual", 1.0m);
            withdrawnBook.Withdraw();
            StudentMember student1 = new StudentMember(2, "Hassan", "01100000000");
            Loan invalidLoan = new Loan(8001, student1, withdrawnBook, DateTime.Now);
        }
        catch (LibraryDomainException ex)
        {
            Console.WriteLine($"[REJECTED]: {ex.Message}");
        }

        // Test B: Borrow an Item Already On Loan
        try
        {
            Console.Write("Attempting to borrow an item already on loan: ");
            Book activeBook = new Book(105, "C# in Depth", 3.0m);
            StudentMember studentA = new StudentMember(3, "Ali", "01111111111");
            StudentMember studentB = new StudentMember(4, "Mona", "01222222222");

            Loan loanA = new Loan(8002, studentA, activeBook, DateTime.Now);
            Loan loanB = new Loan(8003, studentB, activeBook, DateTime.Now); // Must fail
        }
        catch (LibraryDomainException ex)
        {
            Console.WriteLine($"[REJECTED]: {ex.Message}");
        }

        // Test C: Student borrowing a 4th item (Max allowed is 3)
        try
        {
            Console.Write("Attempting student 4th loan (Limit is 3): ");
            StudentMember studentLimitTest = new StudentMember(5, "Youssef", "01555555555");
            Book b1 = new Book(301, "Book 1", 2.0m);
            Book b2 = new Book(302, "Book 2", 2.0m);
            Book b3 = new Book(303, "Book 3", 2.0m);
            Book b4 = new Book(304, "Book 4", 2.0m);

            Loan l1 = new Loan(9001, studentLimitTest, b1, DateTime.Now);
            Loan l2 = new Loan(9002, studentLimitTest, b2, DateTime.Now);
            Loan l3 = new Loan(9003, studentLimitTest, b3, DateTime.Now);
            Loan l4 = new Loan(9004, studentLimitTest, b4, DateTime.Now); // Must fail
        }
        catch (LibraryDomainException ex)
        {
            Console.WriteLine($"[REJECTED]: {ex.Message}");
        }

        // Test D: Return the same loan twice
        try
        {
            Console.Write("Attempting to return the same loan twice: ");
            StudentMember studentRet = new StudentMember(6, "Kareem", "01033333333");
            Magazine mag = new Magazine(401, "National Geographic", 2.0m);
            Loan doubleReturnLoan = new Loan(9005, studentRet, mag, DateTime.Now.AddDays(-2));

            doubleReturnLoan.ReturnItem(DateTime.Now); // First Return (Valid)
            doubleReturnLoan.ReturnItem(DateTime.Now); // Second Return (Must fail)
        }
        catch (LibraryDomainException ex)
        {
            Console.WriteLine($"[REJECTED]: {ex.Message}");
        }

        // Test E: Mark a returned loan as lost
        try
        {
            Console.Write("Attempting to mark a returned loan as lost: ");
            StudentMember studentLost = new StudentMember(7, "Noha", "01044444444");
            Book bookLostTest = new Book(501, "Data Structures", 2.0m);
            Loan returnedThenLostLoan = new Loan(9006, studentLost, bookLostTest, DateTime.Now.AddDays(-5));

            returnedThenLostLoan.ReturnItem(DateTime.Now); // Returned (Valid)
            returnedThenLostLoan.MarkAsLost();              // Mark as lost after return (Must fail)
        }
        catch (LibraryDomainException ex)
        {
            Console.WriteLine($"[REJECTED]: {ex.Message}");
        }

        Console.WriteLine("\n=================================================");
        Console.WriteLine("   SIMULATION COMPLETED SUCCESSFULLY!");
        Console.WriteLine("=================================================");
    }
}