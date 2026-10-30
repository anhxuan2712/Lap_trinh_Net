using System.Data;
using Bai5._5.DAL;
using Bai5._5.DTO;

namespace Bai5._5.BUS;

public sealed class InventoryBUS
{
    private readonly InventoryDAL dal = new();

    public DataTable GetVatTu(string keyword = "") => dal.GetVatTu(keyword.Trim());
    public DataTable GetNhaCungCap(string keyword = "") => dal.GetNhaCungCap(keyword.Trim());
    public DataTable GetVatTuTonDuoi(decimal nguong) => dal.GetVatTuTonDuoi(nguong);
    public decimal GetTongGiaTriTonKho() => dal.GetTongGiaTriTonKho();

    public void AddVatTu(VatTuDTO vatTu)
    {
        ValidateVatTu(vatTu);
        dal.AddVatTu(vatTu);
    }

    public void UpdateVatTu(VatTuDTO vatTu)
    {
        ValidateVatTu(vatTu);
        dal.UpdateVatTu(vatTu);
    }

    public void DeleteVatTu(string maVatTu)
    {
        if (string.IsNullOrWhiteSpace(maVatTu))
            throw new ArgumentException("Vui lòng chọn vật tư cần xóa.");
        dal.DeleteVatTu(maVatTu.Trim());
    }

    public void AddNhaCungCap(NhaCungCapDTO nhaCungCap)
    {
        ValidateNhaCungCap(nhaCungCap);
        dal.AddNhaCungCap(nhaCungCap);
    }

    public void UpdateNhaCungCap(NhaCungCapDTO nhaCungCap)
    {
        ValidateNhaCungCap(nhaCungCap);
        if (nhaCungCap.MaNCC <= 0)
            throw new ArgumentException("Mã nhà cung cấp không hợp lệ.");
        dal.UpdateNhaCungCap(nhaCungCap);
    }

    public void DeleteNhaCungCap(int maNCC)
    {
        if (maNCC <= 0)
            throw new ArgumentException("Vui lòng chọn nhà cung cấp cần xóa.");
        dal.DeleteNhaCungCap(maNCC);
    }

    public int SavePhieuKho(PhieuKhoDTO phieu)
    {
        if (phieu.LoaiPhieu is not ("NHAP" or "XUAT"))
            throw new ArgumentException("Loại phiếu phải là NHAP hoặc XUAT.");
        if (phieu.LoaiPhieu == "NHAP" && phieu.MaNCC is null)
            throw new ArgumentException("Phiếu nhập phải chọn nhà cung cấp.");
        if (phieu.ChiTiet.Count == 0)
            throw new ArgumentException("Phiếu kho phải có ít nhất một vật tư.");
        if (phieu.ChiTiet.Any(item => string.IsNullOrWhiteSpace(item.MaVatTu) || item.SoLuong <= 0 || item.DonGia < 0))
            throw new ArgumentException("Mã vật tư không hợp lệ; số lượng phải lớn hơn 0 và đơn giá không được âm.");
        if (phieu.ChiTiet.GroupBy(item => item.MaVatTu, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
            throw new ArgumentException("Mỗi vật tư chỉ được xuất hiện một lần trong một phiếu.");

        return dal.SavePhieuKho(phieu);
    }

    private static void ValidateVatTu(VatTuDTO vatTu)
    {
        if (string.IsNullOrWhiteSpace(vatTu.MaVatTu) || vatTu.MaVatTu.Trim().Length > 20)
            throw new ArgumentException("Mã vật tư bắt buộc và không được quá 20 ký tự.");
        if (string.IsNullOrWhiteSpace(vatTu.TenVatTu) || vatTu.TenVatTu.Trim().Length > 150)
            throw new ArgumentException("Tên vật tư bắt buộc và không được quá 150 ký tự.");
        if (vatTu.DVT?.Trim().Length > 20)
            throw new ArgumentException("Đơn vị tính không được quá 20 ký tự.");
        if (vatTu.SoLuongTon < 0)
            throw new ArgumentException("Số lượng tồn không được âm.");
    }

    private static void ValidateNhaCungCap(NhaCungCapDTO nhaCungCap)
    {
        if (string.IsNullOrWhiteSpace(nhaCungCap.TenNCC) || nhaCungCap.TenNCC.Trim().Length > 150)
            throw new ArgumentException("Tên nhà cung cấp bắt buộc và không được quá 150 ký tự.");
        if (nhaCungCap.DienThoai?.Trim().Length > 15)
            throw new ArgumentException("Số điện thoại không được quá 15 ký tự.");
        if (nhaCungCap.DiaChi?.Trim().Length > 255)
            throw new ArgumentException("Địa chỉ không được quá 255 ký tự.");
    }
}