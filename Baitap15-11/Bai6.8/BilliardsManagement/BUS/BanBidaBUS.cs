using System;
using System.Collections.Generic;
using BilliardsManagement.DAL;
using BilliardsManagement.DTO;

namespace BilliardsManagement.BUS
{
    public class BanBidaBUS
    {
        private readonly BanBidaDAL dal = new BanBidaDAL();
        private readonly HoaDonDAL hoaDonDal = new HoaDonDAL();

        public List<BanBidaDTO> GetAll()
        {
            return dal.GetAll();
        }

        public int MoBan(BanBidaDTO ban)
        {
            if (ban == null) throw new Exception("Vui lòng chọn bàn!");
            if (ban.DangChoi) throw new Exception("Bàn \"" + ban.TenBan + "\" đang có khách chơi!");
            return hoaDonDal.MoBan(ban.MaBan);
        }
    }
}
