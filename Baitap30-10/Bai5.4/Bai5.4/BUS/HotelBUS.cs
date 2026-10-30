using System.Data;
using Bai5._4.DAL;
using Bai5._4.DTO;

namespace Bai5._4.BUS;

public sealed class HotelBUS
{
    private readonly HotelDAL dal = new();

    public DataTable LoadRooms() => dal.LoadRooms();
    public DataTable LoadServices() => dal.LoadServices();
    public DataTable LoadCurrentStays() => dal.LoadCurrentStays();
    public DataTable LoadUsedServices(int receiptId) => dal.LoadUsedServices(receiptId);
    public KhachHangDTO? FindCustomerByCccd(string cccd) => dal.FindCustomerByCccd(cccd);
    public int AddCustomer(KhachHangDTO customer) => dal.AddCustomer(customer);
    public int CheckIn(string roomNumber, int customerId) => dal.CheckIn(roomNumber, customerId);
    public void AddService(int receiptId, int serviceId, int quantity) => dal.AddService(receiptId, serviceId, quantity);
    public decimal Checkout(int receiptId) => dal.Checkout(receiptId);
}
