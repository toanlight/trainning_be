namespace CleanArch.Domain.Exceptions;

/// <summary>
/// Exception được throw khi xảy ra xung đột dữ liệu (ví dụ: tên đã tồn tại).
/// Tương ứng với HTTP 409 Conflict.
/// </summary>
public class ConflictException : Exception
{
    public ConflictException(string entityName, string field, object value)
        : base($"Entity '{entityName}' with {field} = '{value}' already exists.") { }
}
