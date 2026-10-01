using System;

namespace AutoSpeed
{
    class Program
    {
        static void Main()
        {
            var ql = new QuanLyPhuongTien();

            // TC01: Validation NamSanXuat
            try
            {
                var badOto = new OTo("OT001", "BadCar", 1850, 1000000m, 4, 2.0);
                Console.WriteLine("TC01 FAILED: object created when should not.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"TC01 PASSED: Caught expected exception: {ex.Message}");
            }

            // TC02: OTo 5 cho, GiaGoc = 1,000,000,000 => expected 1,420,000,000
            var oto5 = new OTo("OT005", "AutoSpeed", 2020, 1_000_000_000m, 5, 2.4);
            ql.AddPhuongTien(oto5);
            decimal giaOto5 = oto5.TinhGiaLanBanh();
            Console.WriteLine($"TC02: OTo 5 cho, GiaLanBanh = {giaOto5:N0} VNĐ");

            // TC03: Xe may 150cc, GiaGoc = 50,000,000 => expected 51,000,000
            var xm150 = new XeMay("XM150", "Yamaha", 2021, 50_000_000m, 150);
            ql.AddPhuongTien(xm150);
            decimal giaXm = xm150.TinhGiaLanBanh();
            Console.WriteLine($"TC03: XeMay 150cc, GiaLanBanh = {giaXm:N0} VNĐ");

            // TC04: Polymorphism - call TinhGiaLanBanh in loop
            Console.WriteLine("TC04: Display all and compute GiaLanBanh (polymorphism):");
            ql.DisplayAll();

            // TC05: FindMaxGiaLanBanh -> should be the OTo with 1.42B
            var max = ql.FindMaxGiaLanBanh();
            Console.WriteLine($"TC05: Max GiaLanBanh: {max?.GetInfo()}, {max?.TinhGiaLanBanh():N0} VNĐ");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
