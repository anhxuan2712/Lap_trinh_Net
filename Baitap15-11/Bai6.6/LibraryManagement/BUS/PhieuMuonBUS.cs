using System;
using System.Collections.Generic;
using LibraryManagement.DAL;
using LibraryManagement.DTO;

namespace LibraryManagement.BUS
{
    public class PhieuMuonBUS
    {
        private readonly PhieuMuonDAL dal = new PhieuMuonDAL();
        private readonly DocGiaDAL docGiaDal = new DocGiaDAL();

        public List<PhieuMuonDTO> Loc(int maDocGia, string trangThai)
        {
            return dal.GetDanhSach(maDocGia, trangThai ?? "");
        }

        public List<ChiTietMuonDTO> GetChiTiet(int maPhieu)
        {
            return dal.GetChiTiet(maPhieu);
        }

        public int LapPhieu(int maDocGia, DateTime hanTra, List<ChiTietMuonDTO> chiTiet)
        {
            if (maDocGia <= 0)
                throw new Exception("Vui lòng chọn độc giả!");
            if (!docGiaDal.KiemTraTheConHan(maDocGia))
                throw new Exception("Thẻ độc giả đã HẾT HẠN (hoặc chưa có hạn thẻ). Không thể lập phiếu mượn mới!\nVui lòng gia hạn thẻ trước.");
            if (hanTra.Date <= DateTime.Today)
                throw new Exception("Hạn trả phải sau ngày hôm nay!");
            if (chiTiet == null || chiTiet.Count == 0)
                throw new Exception("Phiếu mượn phải có ít nhất 1 đầu sách!");
            foreach (ChiTietMuonDTO ct in chiTiet)
            {
                if (ct.SoLuong <= 0) throw new Exception("Số lượng mượn phải > 0!");
            }

            PhieuMuonDTO phieu = new PhieuMuonDTO
            {
                MaDocGia = maDocGia,
                HanTra = hanTra.Date.AddDays(1).AddSeconds(-1),
                TrangThai = TrangThaiPhieu.DangMuon
            };
            return dal.LapPhieu(phieu, chiTiet);
        }

        public bool TraSach(int maPhieu)
        {
            if (maPhieu <= 0) throw new Exception("Vui lòng chọn phiếu mượn cần trả!");
            return dal.TraSach(maPhieu);
        }

        public int CapNhatQuaHan()
        {
            return dal.CapNhatQuaHan();
        }

        public List<PhieuMuonDTO> GetPhieuQuaHan()
        {
            return dal.GetPhieuQuaHan();
        }

        public List<ThongKeTheLoaiDTO> ThongKeTheoTheLoai()
        {
            return dal.ThongKeTheoTheLoai();
        }

        public int DemLuotMuonTheoTheLoai(string theLoai)
        {
            if (string.IsNullOrWhiteSpace(theLoai)) throw new Exception("Vui lòng chọn thể loại!");
            return dal.DemLuotMuonTheoTheLoai(theLoai);
        }

        public int DemPhieu(string trangThai)
        {
            return dal.DemPhieu(trangThai);
        }
    }
}
