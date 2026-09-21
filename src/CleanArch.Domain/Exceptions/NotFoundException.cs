namespace CleanArch.Domain.Exceptions;

/// <summary>
/// Exception được throw khi không tìm thấy entity trong hệ thống.
/// Tương ứng với HTTP 404 Not Found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"Entity '{entityName}' with key '{key}' was not found.") { }
}
