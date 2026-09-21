namespace CleanArch.Domain.Exceptions;

/// <summary>
/// Base exception cho các business rule violations.
/// Tương ứng với HTTP 400 Bad Request.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
