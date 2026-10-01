using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeed
{
    public class QuanLyPhuongTien
    {
        private readonly List<PhuongTien> _list = new List<PhuongTien>();

        public void AddPhuongTien(PhuongTien pt)
        {
            if (pt == null) throw new ArgumentNullException(nameof(pt));
            _list.Add(pt);
        }

        public void DisplayAll()
        {
            foreach (var pt in _list)
            {
                Console.WriteLine($"{pt.GetInfo()}, GiaLanBanh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        public PhuongTien FindMaxGiaLanBanh()
        {
            return _list.OrderByDescending(p => p.TinhGiaLanBanh()).FirstOrDefault();
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<PhuongTien>(_list);
            string k = keyword.Trim().ToLowerInvariant();
            return _list.Where(p => p.TenHang.ToLowerInvariant().Contains(k)).ToList();
        }
    }
}
