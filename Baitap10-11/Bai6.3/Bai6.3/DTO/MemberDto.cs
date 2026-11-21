namespace Bai6._3.DTO;

public sealed class MemberDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Expertise { get; set; } = "";
    public override string ToString() => $"{FullName} ({Email})";
}
