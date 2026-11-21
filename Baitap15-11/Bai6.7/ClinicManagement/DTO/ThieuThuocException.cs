using System;

namespace ClinicManagement.DTO
{
    public class ThieuThuocException : Exception
    {
        public ThieuThuocException(string message) : base(message) { }
    }
}
