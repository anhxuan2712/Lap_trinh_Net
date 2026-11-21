namespace Bai6._2.DTO;

public sealed record Employee(
    int? Id,
    string Name,
    DateTime? BirthDate,
    string? IdentityNumber,
    int? DepartmentId,
    decimal BaseSalary,
    decimal SalaryCoefficient);