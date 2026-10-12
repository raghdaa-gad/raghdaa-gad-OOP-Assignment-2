using System;

namespace LibrarySystem;

public class LibraryDomainException : Exception
{
    public LibraryDomainException (string message ):base(message)
    {
        
    }
}