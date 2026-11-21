using System;
using System.Collections.Generic;
using ClinicManagement.DAL;
using ClinicManagement.DTO;

namespace ClinicManagement.BUS
{
    public class HoSoKhamBUS
    {
        private readonly HoSoKhamDAL dal = new HoSoKhamDAL();
        private readonly BenhNhanBUS benhNhanBus = new BenhNhanBUS();

        public int TiepNhan(BenhNhanDTO bn, string trieuChung)
        {
            trieuChung = (trieuChung ?? "").Trim();
            if (trieuChung == "") throw new Exception("Vui lòng nhập triệu chứng của bệnh nhân!");
            if (bn == null) throw new Exception("Chưa có thông tin bệnh nhân!");

            if (bn.MaBN <= 0)
                bn.MaBN = benhNhanBus.Them(bn);

            return dal.Insert(bn.MaBN, trieuChung);
        }

        public List<HoSoKhamDTO> GetByBenhNhan(int maBN)
        {
            return dal.GetByBenhNhan(maBN);
        }

        public List<HoSoKhamDTO> GetChoKeDon()
        {
            return dal.GetChoKeDon();
        }
    }
}
