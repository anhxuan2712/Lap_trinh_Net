using System;

namespace AutoSpeed
{
    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get => _dungTichXylanh;
            set
            {
                if (value <= 0) throw new ArgumentException("DungTichXylanh must be > 0.");
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            decimal g = GiaGoc;
            if (DungTichXylanh < 175)
            {
                return g + (g * 0.02m);
            }
            else
            {
                return g + (g * 0.05m);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", DungTichXylanh: {DungTichXylanh} cc";
        }
    }
}
