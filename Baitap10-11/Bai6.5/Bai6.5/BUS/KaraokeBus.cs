using System.Data;
using Bai6._5.DAL;
using Bai6._5.DTO;

namespace Bai6._5.BUS;

/// <summary>
/// Lớp nghiệp vụ: kiểm tra quy tắc, tính tiền và điều phối giao dịch.
/// GUI chỉ gọi lớp này, không gọi trực tiếp DAL.
/// </summary>
public sealed class KaraokeBus
{
    private const string RoomEmpty = "Trống";
    private const string RoomOccupied = "Đang hát";

    private readonly KaraokeDal dal = new();

    public List<RoomDto> GetRooms() => dal.GetRooms();
    public List<ServiceDto> GetServices() => dal.GetServices();
    public DataTable GetOrderItems(int invoiceId) => dal.GetOrderItems(invoiceId);

    // ===== Kiểm tra lựa chọn trên giao diện =====

    public RoomDto EnsureRoomSelected(RoomDto? room) =>
        room ?? throw new InvalidOperationException("Vui lòng chọn phòng.");

    public int EnsureRoomOccupied(RoomDto? room, string message = "Phòng này hiện không có hóa đơn đang mở.")
    {
        var selected = room ?? throw new InvalidOperationException("Vui lòng chọn phòng đang hát.");
        return selected.ActiveInvoiceId ?? throw new InvalidOperationException(message);
    }

    // ===== Mở phòng =====

    public void OpenRoom(RoomDto? room)
    {
        int roomId = EnsureRoomSelected(room).Id;

        using var connection = dal.OpenConnection();
        using var transaction = connection.BeginTransaction();

        var status = dal.GetRoomStatusForUpdate(transaction, roomId)
            ?? throw new InvalidOperationException("Không tìm thấy phòng.");

        if (status != RoomEmpty)
            throw new InvalidOperationException("Phòng này hiện không trống.");

        if (dal.CountOpenInvoices(transaction, roomId) > 0)
            throw new InvalidOperationException("Phòng đã có hóa đơn chưa thanh toán.");

        dal.InsertInvoice(transaction, roomId);

        if (dal.UpdateRoomStatus(transaction, roomId, RoomOccupied) != 1)
            throw new InvalidOperationException("Không thể cập nhật trạng thái phòng.");

        transaction.Commit();
    }

    // ===== Gọi dịch vụ =====

    public void AddService(RoomDto? room, ServiceDto? service, int quantity)
    {
        int invoiceId = EnsureRoomOccupied(room, "Chỉ có thể gọi dịch vụ cho phòng đang hát.");

        if (service is null)
            throw new InvalidOperationException("Vui lòng chọn dịch vụ.");

        if (quantity <= 0)
            throw new InvalidOperationException("Số lượng phải lớn hơn 0.");

        using var connection = dal.OpenConnection();
        using var transaction = connection.BeginTransaction();

        var invoiceStatus = dal.GetInvoiceStatusForUpdate(transaction, invoiceId);
        if (invoiceStatus != 0)
            throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã thanh toán.");

        var current = dal.GetServiceForUpdate(transaction, service.Id)
            ?? throw new InvalidOperationException("Không tìm thấy dịch vụ đã chọn.");

        int pendingQuantity = dal.GetPendingQuantity(transaction, service.Id);
        if (pendingQuantity + quantity > current.Stock)
            throw new InvalidOperationException("Tồn kho không đủ.");

        dal.UpsertOrderItem(transaction, invoiceId, service.Id, quantity, current.UnitPrice);

        transaction.Commit();
    }

    // ===== Thanh toán =====

    public PaymentResultDto PayRoom(RoomDto? room)
    {
        int invoiceId = EnsureRoomOccupied(room);
        int roomId = room!.Id;

        using var connection = dal.OpenConnection();
        using var transaction = connection.BeginTransaction();

        var invoice = dal.GetOpenInvoiceForUpdate(transaction, roomId, invoiceId)
            ?? throw new InvalidOperationException("Không tìm thấy hóa đơn đang mở.");

        DateTime now = DateTime.Now;
        decimal roomTotal = CalculateRoomTotal(invoice.HourlyRate, invoice.StartedAt, now);

        var lines = dal.GetOrderLines(transaction, invoiceId);
        decimal serviceTotal = lines.Sum(line => line.Amount);

        foreach (var line in lines.OrderBy(line => line.ServiceId))
        {
            if (dal.DecreaseStock(transaction, line.ServiceId, line.Quantity) != 1)
                throw new InvalidOperationException("Tồn kho không đủ để thanh toán.");
        }

        if (dal.CloseInvoice(transaction, invoiceId, now, roomTotal, serviceTotal) != 1)
            throw new InvalidOperationException("Hóa đơn không còn hợp lệ.");

        if (dal.UpdateRoomStatus(transaction, roomId, RoomEmpty) != 1)
            throw new InvalidOperationException("Không thể cập nhật trạng thái phòng.");

        transaction.Commit();

        return new PaymentResultDto
        {
            InvoiceId = invoiceId,
            RoomTotal = roomTotal,
            ServiceTotal = serviceTotal
        };
    }

    /// <summary>Tiền phòng = giá theo giờ × số giờ thực tế (tính theo giây), làm tròn 2 chữ số.</summary>
    public static decimal CalculateRoomTotal(decimal hourlyRate, DateTime startedAt, DateTime endedAt)
    {
        decimal seconds = Convert.ToDecimal(Math.Max(0, (endedAt - startedAt).TotalSeconds));
        return Math.Round(hourlyRate * seconds / 3600m, 2, MidpointRounding.AwayFromZero);
    }
}
