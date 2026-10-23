using System;
using System.ComponentModel;

namespace test2._2_chieu_23_10
{
    public class EmployeeModel
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public string AvatarPath { get; set; }

        public decimal TotalSalary => BaseSalary + Allowance;

        public string ToCsvLine()
        {
            string safeName = FullName?.Replace(",", ";") ?? string.Empty;
            string safeDept = Department?.Replace(",", ";") ?? string.Empty;
            string safePos = Position?.Replace(",", ";") ?? string.Empty;
            string safePath = AvatarPath?.Replace(",", ";") ?? string.Empty;

            return string.Format("{0},{1},{2},{3},{4},{5},{6}",
                EmployeeId,
                safeName,
                safeDept,
                safePos,
                BaseSalary,
                Allowance,
                safePath);
        }

        public static EmployeeModel FromCsvLine(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length < 6)
            {
                return null;
            }

            decimal baseSalary = 0;
            decimal allowance = 0;
            decimal.TryParse(parts[4], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out baseSalary);
            decimal.TryParse(parts[5], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out allowance);

            return new EmployeeModel
            {
                EmployeeId = parts[0].Trim(),
                FullName = parts[1].Trim(),
                Department = parts[2].Trim(),
                Position = parts[3].Trim(),
                BaseSalary = baseSalary,
                Allowance = allowance,
                AvatarPath = parts.Length >= 7 ? parts[6].Trim() : string.Empty
            };
        }
    }
}
