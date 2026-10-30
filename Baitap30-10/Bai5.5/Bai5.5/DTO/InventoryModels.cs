namespace Bai5._5.DTO;

public sealed class VatTuDTO
{
    public string MaVatTu { get; set; } = string.Empty;
    public string TenVatTu { get; set; } = string.Empty;
    public string DVT { get; set; } = string.Empty;
    public decimal SoLuongTon { get; set; }
}

public sealed class NhaCungCapDTO
{
    public int MaNCC { get; set; }
    public string TenNCC { get; set; } = string.Empty;
    public string? DienThoai { get; set; }
    public string? DiaChi { get; set; }
}

public sealed class ChiTietPhieuKhoDTO
{
    public string MaVatTu { get; set; } = string.Empty;
    public string TenVatTu { get; set; } = string.Empty;
    public decimal SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLuong * DonGia;
}

public sealed class PhieuKhoDTO
{
    public string LoaiPhieu { get; set; } = string.Empty;
    public int? MaNCC { get; set; }
    public List<ChiTietPhieuKhoDTO> ChiTiet { get; set; } = [];
}