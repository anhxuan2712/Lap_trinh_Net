namespace Bai5._4.DTO;

public sealed class PhieuDatPhongDTO
{
    public int MaPhieu { get; set; }
    public string SoPhong { get; set; } = string.Empty;
    public int MaKH { get; set; }
    public DateTime NgayNhanPhong { get; set; }
    public DateTime? NgayTraPhong { get; set; }
    public decimal TongTienThanhToan { get; set; }
}
