using System;

namespace AutoSpeed
{
    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get => _soChoNgoi;
            set
            {
                if (value <= 0) throw new ArgumentException("SoChoNgoi must be > 0.");
                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get => _dungTichDongCo;
            set
            {
                if (value <= 0) throw new ArgumentException("DungTichDongCo must be > 0.");
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            decimal g = GiaGoc;
            if (SoChoNgoi <= 9)
            {
                return g + (g * 0.12m) + (g * 0.30m);
            }
            else
            {
                return g + (g * 0.10m);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", SoChoNgoi: {SoChoNgoi}, DungTichDongCo: {DungTichDongCo} L";
        }
    }
}
